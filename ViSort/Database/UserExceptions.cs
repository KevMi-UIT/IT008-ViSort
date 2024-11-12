namespace ViSort.Database;

internal static class UserExceptions
{

    internal class PasswordDoesNotMatch(string message) : Exception(message)
    {
    }

    internal class UserNotFound(string message) : Exception(message)
    {
    }
}