namespace ViSort.Database;

public static class UserExceptions
{
    public class PasswordDoesNotMatch(string message) : Exception(message)
    {
    }

    public class UserNotFound(string message) : Exception(message)
    {
    }

    public class UserAlreadyExists(string message) : Exception(message)
    {
    }

    public class UserNoChanges(string message) : Exception(message)
    {
    }
}