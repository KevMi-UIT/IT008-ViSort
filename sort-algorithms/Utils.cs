namespace SortUtils;

public static class SortUtils
{
    public static void swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }

    public static void swap<T>(List<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }
}


