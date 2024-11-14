﻿namespace ViSort.Utils;

internal static class Utils
{
    internal const int MAX_SCORE = 999;
    internal const double SCORE_SENSITIVITY = 0.01;

    internal static int CalcScore(int steps, int questions, int answered)
    {
        return Convert.ToInt32(MAX_SCORE / (1 + (steps * SCORE_SENSITIVITY * (questions / answered))));
    }

    internal static void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }

    internal static void Swap<T>(List<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }

    // https://stackoverflow.com/a/3456788/23173098
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random rnd = new();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }
}