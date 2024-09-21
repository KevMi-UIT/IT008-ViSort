public static class QuickSort
{
    private static int Partition<T>(List<T> list, int low, int high) where T : IComparable<T>
    {
        T pivot = list[high];
        int i = low - 1;
        for (int j = low; j <= high - 1; j++)
        {
            if (list[j].CompareTo(pivot) < 0)
            {
                i++;
                swap(list, i, j);
            }
        }
        swap(list, i + 1, high);
        return i + 1;
    }

    public static void Recursive<T>(List<T> list, int low, int high) where T : IComparable<T>
    {
        if (low < high)
        {
            int pi = Partition(list, low, high);
            Recursive(list, low, pi - 1);
            Recursive(list, pi + 1, high);
        }
    }

    public static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        Recursive(list, 0, list.Count - 1);
    }
}



