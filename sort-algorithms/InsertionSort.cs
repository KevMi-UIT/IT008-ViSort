public static class InsertionSort
{
    public static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        int N = list.Count;
        for (int i = 1; i < N; ++i)
        {
            T Key = list[i];
            int j = i - 1;
            while (j >= 0 && list[j].CompareTo(Key) > 0)
            {
                list[j + 1] = list[j];
                j = j - 1;
            }
            list[j + 1] = Key;
        }
    }
}