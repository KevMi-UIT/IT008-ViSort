//public static class TimSort
//{
//    public const int Run = 32;

//    public static void InsertionSort(List<int> list, int left, int right) where T : IComparable 
//    { 
//        for (int i = left + 1; i <= right; i++) 
//        { 
//            T Temp = list[i]; 
//            int j = i - 1; 
//            while (j >= left && list[j].CompareTo(Temp) > 0) 
//            { 
//                list[j + 1] = list[j]; 
//                j--; 
//            } 
//            list[j + 1] = Temp; 
//        } 
//    } 

//    public static void Merge(List<int> list, int left, int mid, int right) where T : IComparable
//    {
//        int i, j, k;
//        int Length1 = mid - left + 1, Length2 = right - mid;
//        T[] LeftArr = new T[Length1];
//        T[] RightArr = new T[Length2];
//        for (i = 0; i < Length1; i++)
//            LeftArr[i] = list[left + i];
//        for (i = 0; i < Length2; i++)
//            RightArr[i] = list[mid + 1 + i];
//        i = 0;
//        j = 0;
//        k = 0;
//        while (i  < Length1 && j < Length2)
//        {
//            if (LeftArr[i].CompareTo(RightArr[j]) <= 0)
//            {
//                list[k] = LeftArr[i];
//                i++;
//            }
//            else
//            {
//                list[k] = RightArr[j];
//                j++;
//            }
//            k++;
//        }
//        while (i < Length1)
//        {
//            list[k] = LeftArr[i];
//            k++;
//            i++;
//        }
//        while (j < Length2)
//        {
//            list[k] = RightArr[j];
//            k++;
//            j++;
//        }
//    }

//    public static void Sort (List<int> list) where T : IComparable
//    {
//        int N = list.Count;
//        for (int i = 0; i < N; i+= Run)
//        {
//            InsertionSort(list, i, Math.Min(i + Run - 1, (N - 1)));
//        }
//        for (int size = Run; size < N; size = size * 2)
//        {
//            for (int left = 0; left < N; left += size * 2)
//            {
//                int mid = left + size - 1;
//                int right = Math.Min((left + size * 2 - 1), (N - 1));
//                if (mid < right)
//                    Merge(list, left, mid, right);
//            }
//        }
//    }
//}