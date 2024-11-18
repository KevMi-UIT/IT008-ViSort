using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
using System.Text;
using ViSort.Utils;

namespace ViSort.Models;

public class UserModel
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; private set; }

    [BsonElement("password")]
    public string HashedPassword { get; private set; }

    private string _password;
    public string Password
    {
        get => _password;
        private set
        {
            _password = value;
            HashedPassword = Hasher.Hash(value);
        }
    }

    [BsonElement("score")]
    public int Score { get; private set; }

    public UserModel(string username, string password, int score = 0)
    {
        Username = username;
        _password = password;
        HashedPassword = Hasher.Hash(password);
        Score = score;
    }

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
        throw new NotImplementedException();
    }
}