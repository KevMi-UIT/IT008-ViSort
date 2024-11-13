namespace ViSort.Database;

internal static class UserExceptions
{

    public class PasswordDoesNotMatch(string message) : Exception(message)
    {
    }

    public class UserNotFound(string message) : Exception(message)
    {
    }
}