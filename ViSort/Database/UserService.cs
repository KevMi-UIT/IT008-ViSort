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
        var client = new MongoClient(config["db:connectionString"]);
        var database = client.GetDatabase(config["db:dbName"]);
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
        var projection = Builders<UserModel>.Projection.Include(u => u.Username).Include(u => u.Score != 0);
        return await UsersCollection.Find(_ => true).Project<UserModel>(projection).SortByDescending(u => u.Score).ToListAsync();
    }

    public async Task<bool> AuthUserAsync(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
        var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        if (existingUser == null)
        {
            await AddUserAsync(user);
            return true;
        }
        return user.EncryptedPassword == existingUser.EncryptedPassword;
    }

    public async Task UpdateScoreAsync(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
        var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        var update = Builders<UserModel>.Update.Set(u => u.Score, user.Score);
        await UsersCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteUserAsync(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
        var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        UsersCollection.DeleteOne(filter);
    }
}