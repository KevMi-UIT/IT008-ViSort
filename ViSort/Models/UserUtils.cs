using System.Text.RegularExpressions;

namespace ViSort.Models;

internal static class UserUtils
{
    private static readonly Regex UsernameRegex = new Regex(@"^[A-Za-z0-9]+$");
    private static readonly Regex PasswordRegex = new Regex(@"^.{4,}$");

    public static bool ValidateUsername(string username)
    {
        return UsernameRegex.IsMatch(username);
    }

    public static bool ValidatePassword(string password)
    {
        return PasswordRegex.IsMatch(password);
    }
}
