using System.Diagnostics;

namespace ViSort.Utils;

public static class Utils
{
    public const int SCORE_SENSITIVITY = 9999;

    public static int CalcScore(int steps, int questions, int answered)
    {
        return Convert.ToInt32((double)answered / questions / (steps + 1) * SCORE_SENSITIVITY);
    }

    public static void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }

    public static void Swap<T>(List<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }

    // https://stackoverflow.com/a/3456788/23173098
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random rnd = new();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public static void OpenLink(string url)
    {
        var sInfo = new ProcessStartInfo(url)
        {
            UseShellExecute = true,
        };
        Process.Start(sInfo);
    }
}