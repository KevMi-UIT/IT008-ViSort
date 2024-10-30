using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViSort.Models;

namespace ViSort.Database
{
    internal class UserService
    {
        private readonly IMongoCollection<UserModel> UsersCollection;

        public UserService()
        {
            var client = new MongoClient(DatabaseConstants.ConnectionString);
            var database = client.GetDatabase(DatabaseConstants.Database);
            UsersCollection = database.GetCollection<UserModel>(DatabaseConstants.UsersCollection);
        }

        public async Task SaveOrUpdateUser(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
            var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                if (UserModel.VerifyPassword(user.Password, existingUser.Password))
                {
                    var update = Builders<UserModel>.Update.Set(u => u.Score, user.Score);
                    await UsersCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    throw new System.UnauthorizedAccessException("Password does not match.");
                }
            }
            else
            {
                await UsersCollection.InsertOneAsync(user);
            }
        }

        public async Task<List<UserModel>> GetAllUsersResult()
        {
            var projection = Builders<UserModel>.Projection.Include(u => u.Username).Include(u => u.Score);
            return await UsersCollection.Find(_ => true).Project<UserModel>(projection).SortByDescending(u => u.Score).ToListAsync();
        }

        public async void DeleteUser(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username);
            var existingUser = await UsersCollection.Find(filter).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                if (UserModel.VerifyPassword(user.Password, existingUser.Password))
                {
                    UsersCollection.DeleteOne(filter);
                }
                else
                {
                    throw new UnauthorizedAccessException("Password does not match.");
                }
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
