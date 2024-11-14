//public static class BucketSort
//{
//    public static void InsertionSort<T>(List<T> list) where T : IComparable<T>
//    {
//        int N = list.Count;
//        for (int i = 1; i < N; ++i)
//        {
//            T Key = list[i];
//            int j = i - 1;
//            while (j >= 0 && list[j].CompareTo(Key) > 0)
//            {
//                list[j + 1] = list[j];
//                j = j - 1;
//            }
//            list[j + 1] = Key;
//        }
//    }

//    public static List<T> Sort<T>(List<T> list) where T : IComparable<T>, IConvertible
//    {
//        int num_of_bucket = list.Count;
//        T minValue = list[0];
//        T maxValue = list[0];

//        foreach (var item in list)
//        {
//            if (item.CompareTo(minValue) < 0) minValue = item;
//            if (item.CompareTo(maxValue) > 0) maxValue = item;
//        }

//        List<double>[] buckets = new List<double>[num_of_bucket];
//        for (int i = 0; i < num_of_bucket; i++)
//        {
//            buckets[i] = new List<double>();
//        }

//        foreach (var item in list)
//        {
//            double normalizedValue = (Convert.ToDouble(item) - Convert.ToDouble(minValue)) / 
//                                    (Convert.ToDouble(maxValue) - Convert.ToDouble(minValue));
//            int bucket = (int)(normalizedValue * (num_of_bucket - 1));
//            buckets[bucket].Add(normalizedValue);
//        }

//        List<T> sortedArray = new List<T>();
//        for (int i = 0; i < num_of_bucket; i++)
//        {
//            InsertionSort(buckets[i]);
//            foreach (var value in buckets[i])
//            {
//                double denormalizedValue = value * (Convert.ToDouble(maxValue) - 
//                Convert.ToDouble(minValue)) + Convert.ToDouble(minValue);
//                sortedArray.Add((T)Convert.ChangeType(denormalizedValue, typeof(T)));
//            }
//        }

//        return sortedArray;
//    }
//}
