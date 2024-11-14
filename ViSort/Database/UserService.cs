using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ViSort.Models;
using static ViSort.Database.UserExceptions;

namespace ViSort.Database;

public class UserService
{
    private readonly IMongoCollection<UserModel> UsersCollection;

    public UserService()
    {
        var config = App.Config!;
        MongoClient client = new(config["db:connectionString"]);
        IMongoDatabase database = client.GetDatabase(config["db:dbName"]);
        UsersCollection = database.GetCollection<UserModel>(config["db:userCollection"]);
    }

    private static void CheckUser(UserModel user, UserModel userDB)
    {
        if (userDB == null)
        {
            throw new UserNotFound("User not found.");
        }
        if (user.EncryptedPassword != userDB.EncryptedPassword)
        {
            throw new PasswordDoesNotMatch("Password does not match.");
        }
    }

    private async Task AddUserAsync(UserModel user)
    {
        await UsersCollection.InsertOneAsync(user);
    }

    public async Task<List<UserModel>> GetAllUsersResultAsync()
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Ne(static u => u.Score, 0);
        ProjectionDefinition<UserModel>? projection = Builders<UserModel>.Projection.Include(static u => u.Username).Include(static u => u.Score);
        return await UsersCollection.Find(filter).Project<UserModel>(projection).SortByDescending(static u => u.Score).ToListAsync();
    }

    public async Task AuthUserAsync(UserModel user)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, user.Username);
        UserModel? existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        if (existingUser == null)
        {
            await AddUserAsync(user);
            return;
        }
        if (user.EncryptedPassword != existingUser.EncryptedPassword)
        {
            throw new PasswordDoesNotMatch("Password does not match");
        }
    }

    public async Task UpdateScoreAsync(UserModel user)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, user.Username);
        UserModel? existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        UpdateDefinition<UserModel>? update = Builders<UserModel>.Update.Set(static u => u.Score, user.Score);
        _ = await UsersCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteUserAsync(UserModel user)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, user.Username);
        UserModel? existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        _ = await UsersCollection.DeleteOneAsync(filter);
    }
}