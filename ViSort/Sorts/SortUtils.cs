using System.Collections.Generic;

namespace ViSort.Sorts
{
    internal static class SortUtils
    {
        internal static void Swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }

        internal static void Swap<T>(List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
