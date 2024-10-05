public static class BubbleSort 
{
    public static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        int i, j;
        int N = list.Count;
        bool swapped;
        for (i = 0; i < N - 1; i++) 
        {
            swapped = false;
            for (j = 0; j < N - i - 1; j++) 
            {
                if (list[j].CompareTo(list[j + 1]) > 0) 
                {
                    swap(list, j, j+1);
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }
}
