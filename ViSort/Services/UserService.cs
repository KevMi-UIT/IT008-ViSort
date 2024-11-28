using MongoDB.Driver;
using ViSort.Models;
using ViSort.Utils;
using static ViSort.Exceptions.UserExceptions;

namespace ViSort.Database;

public class UserService
{
    private readonly IMongoCollection<UserModel> UsersCollection;

    public UserService()
    {
        var config = App.Config;
        MongoClient client = new(config["db:connectionString"]);
        IMongoDatabase database = client.GetDatabase(config["db:dbName"]);
        UsersCollection = database.GetCollection<UserModel>(config["db:userCollection"]);
    }

    private async Task<UserModel?> GetUserFromDBAsync(string username)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, username);
        return await UsersCollection.Find(filter).FirstOrDefaultAsync();
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
        UserModel? existingUser = await GetUserFromDBAsync(user.Username);
        if (existingUser == null)
        {
            await AddUserAsync(user);
            return;
        }
        if (!Hasher.Verify(user.Password, existingUser.HashedPassword))
        {
            throw new PasswordDoesNotMatch("Password does not match");
        }
    }

    public async Task UpdateScoreAsync(UserModel user)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, user.Username);
        UserModel? existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        if (!Hasher.Verify(user.Password, existingUser.HashedPassword))
        {
            throw new PasswordDoesNotMatch("Password does not match");
        }
        UpdateDefinition<UserModel>? update = Builders<UserModel>.Update.Set(static u => u.Score, user.Score);
        await UsersCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteUserAsync(UserModel user)
    {
        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(static u => u.Username, user.Username);
        await UsersCollection.DeleteOneAsync(filter);
    }

    public async Task ChangeUserProfileAsync(UserModel oldUser, UserModel newUser)
    {
        if (oldUser.Equals(newUser))
        {
            throw new UserNoChanges("No modification on user.");
        }
        UserModel? existingUser = await GetUserFromDBAsync(newUser.Username);
        if (existingUser != null && oldUser.Username != newUser.Username)
        {
            throw new UserAlreadyExists("User already exists.");
        }
        await DeleteUserAsync(oldUser);
        await AddUserAsync(newUser);
    }
}