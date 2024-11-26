using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ViSort.Utils;

namespace ViSort.Models;

public class UserModel(string username, string password, int score = 0)
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; private set; } = username;

    [BsonElement("password")]
    public string HashedPassword { get; private set; } = Hasher.Hash(password);

    [BsonIgnore]
    public string Password
    {
        get => password;
        set
        {
            password = value;
            HashedPassword = Hasher.Hash(value);
        }
    }

    [BsonElement("score")]
    public int Score { get; private set; } = score;

    public void UpdatePassword(string password)
    {
        Password = password;
    }

    public void SetScore(int score)
    {
        Score = score;
    }

    public override bool Equals(object? obj)
    {
        if (obj is UserModel user)
        {
            return user.Username == Username && user.Password == Password;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Username.GetHashCode();
    }
}