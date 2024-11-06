using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViSort.Models;

namespace ViSort.Database;

internal class UserService
{
    private readonly IMongoCollection<UserModel> UsersCollection;

    internal UserService()
    {
        var client = new MongoClient(DatabaseConstants.ConnectionString);
        var database = client.GetDatabase(DatabaseConstants.Database);
        UsersCollection = database.GetCollection<UserModel>(DatabaseConstants.UsersCollection);
    }

    private static void CheckUser(UserModel user, UserModel userDB)
    {
        if (userDB == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }
        if (user.EncryptedPassword != userDB.EncryptedPassword)
        {
            throw new UnauthorizedAccessException("Password does not match.");
        }
    }

    private async Task AddUserAsync(UserModel user)
    {
        await UsersCollection.InsertOneAsync(user);
    }

    internal async Task<List<UserModel>> GetAllUsersResult()
    {
        var projection = Builders<UserModel>.Projection.Include(u => u.Username).Include(u => u.Score != 0);
        return await UsersCollection.Find(_ => true).Project<UserModel>(projection).SortByDescending(u => u.Score).ToListAsync();
    }

    internal async Task<bool> AuthUserAsync(UserModel user)
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

    internal async Task UpdateScore(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
        var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        var update = Builders<UserModel>.Update.Set(u => u.Score, user.Score);
        await UsersCollection.UpdateOneAsync(filter, update);
    }

    internal async void DeleteUserAsync(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
        var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
        CheckUser(user, existingUser);
        UsersCollection.DeleteOne(filter);
    }
}