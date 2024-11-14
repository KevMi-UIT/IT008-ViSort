using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
using System.Text;

namespace ViSort.Models;

public class UserModel
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; private set; }

    [BsonElement("password")]
    public string EncryptedPassword { get; private set; }

    private string _password;
    public string Password
    {
        get => _password;
        private set
        {
            _password = value;
            EncryptedPassword = EncryptPassword(value);
        }
    }

    [BsonElement("score")]
    public int Score { get; private set; }

    public UserModel(string username, string password, int score = 0)
    {
        Username = username;
        _password = password;
        EncryptedPassword = EncryptPassword(password);
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

    public static string EncryptPassword(string unencryptedPassword)
    {
        using Aes aes = Aes.Create();
        aes.Key = GetHash(unencryptedPassword); // Create the AES key using the password's hash
        aes.GenerateIV();
        byte[] iv = aes.IV;

        // Encrypt the password
        using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(unencryptedPassword);
        byte[] encryptedPassword = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

        // Combine IV and encrypted password for storage
        byte[] result = new byte[iv.Length + encryptedPassword.Length];
        Buffer.BlockCopy(iv, 0, result, 0, Buffer.ByteLength(iv));
        Buffer.BlockCopy(encryptedPassword, 0, result, iv.Length, Buffer.ByteLength(encryptedPassword));

        return Convert.ToBase64String(result);
    }

    public static bool VerifyPassword(string inputPassword, string storedEncryptedPassword)
    {
        try
        {
            // Convert the base64 encrypted password back to byte array
            byte[] fullCipher = Convert.FromBase64String(storedEncryptedPassword);

            // Extract IV from stored password
            byte[] iv = new byte[16];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, Buffer.ByteLength(iv));

            // Extract encrypted password bytes
            byte[] encryptedPassword = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, iv.Length, encryptedPassword, 0, Buffer.ByteLength(encryptedPassword));

            // Decrypt password
            using Aes aes = Aes.Create();
            aes.Key = GetHash(inputPassword); // Generate key from input password
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decryptedPasswordBytes = decryptor.TransformFinalBlock(encryptedPassword, 0, encryptedPassword.Length);
            string decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);

            return inputPassword == decryptedPassword;
        }
        catch
        {
            return false; // If decryption fails, password does not match
        }
    }

    // Generates a 256-bit hash key from the password
    private static byte[] GetHash(string password)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(password));
    }
}