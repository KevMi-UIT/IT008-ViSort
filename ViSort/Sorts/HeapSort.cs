//public static class HeapSort
//{
//    public static void Sort<T>(List<T> list) where T : IComparable<T>
//    {
//        int N = list.Count;
//        for (int i = (N / 2) - 1; i >= 0; i--)
//        {
//            Heapify(list, N, i);
//        }
//        for (int i = N - 1; i > 0; i--)
//        {
//            swap(list, 0, i);
//            Heapify(list, i, 0);
//        }
//    }

//    public static void Heapify<T>(List<T> list, int N, int i) where T : IComparable<T>
//    {
//        int largest = i;
//        int l = (2 * i) + 1;
//        int r = (2 * i) + 2;
//        if (l < N && list[l].CompareTo(list[largest]) > 0)
//        {
//            largest = l;
//        }
//        if (r < N && list[r].CompareTo(list[largest]) > 0)
//        {
//            largest = r;
//        }
//        if (largest != i)
//        {
//            swap(list, i, largest);
//            Heapify(list, N, largest);
//        }
//    }
//}

