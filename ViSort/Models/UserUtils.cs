using System.Text.RegularExpressions;

namespace ViSort.Models;

internal static partial class UserUtils
{
    [GeneratedRegex(@"^[A-Za-z0-9]+$")]
    private static partial Regex UsernameRegex();

    [GeneratedRegex(@"^.{4,}$")]
    private static partial Regex PasswordRegex();

    public static bool ValidateUsername(string username)
    {
        return UsernameRegex().IsMatch(username);
    }

    public static bool ValidatePassword(string password)
    {
        return PasswordRegex().IsMatch(password);
    }
}
