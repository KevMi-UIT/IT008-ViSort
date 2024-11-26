using System.Security.Cryptography;

namespace ViSort.Utils;

// https://stackoverflow.com/a/73125177/23173098
public static class Hasher
{
    private const int SALT_SIZE = 16; // 128 bits
    private const int KEY_SIZE = 32; // 256 bits
    private const int ITERATIONS = 50000;
    private static readonly HashAlgorithmName ALGORITHM = HashAlgorithmName.SHA256;

    private const char segmentDelimiter = ':';

    public static string Hash(string input)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            ITERATIONS,
            ALGORITHM,
            KEY_SIZE
        );
        return string.Join(
            segmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            ITERATIONS,
            ALGORITHM
        );
    }

    public static bool Verify(string input, string hashString)
    {
        string[] segments = hashString.Split(segmentDelimiter);
        byte[] hash = Convert.FromHexString(segments[0]);
        byte[] salt = Convert.FromHexString(segments[1]);
        if (int.TryParse(segments[2], out int iterations))
        {
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
        return false;
    }
}