public static class SelectionSort 
{
    public static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        int N = list.Count;
        for (int i = 0; i < N - 1; i++) 
        {
            int min_idx = i;
            for (int j = i + 1; j < N; j++) 
            {
                if (list[j].CompareTo(list[min_idx]) < 0) 
                {
                    min_idx = j;
                }
            }
            if (min_idx != i) 
            {
                swap(list, i, min_idx);
            }
        }
    }
}
