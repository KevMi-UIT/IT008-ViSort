using System;
using System.Collections.Generic;
using System.Linq;

namespace ViSort.Utils
{
    internal static class GenRandomList
    {
        enum RandomGenType
        {
            Normal,
            Sorted,
            SortedReverse,
            NearlySorted,
            Mirror
        }
        private static readonly int MIN = 0;
        private static readonly int MAX = 999;
        private static readonly int NEARLY_SORTED_SENSITIVITY = 10;
        private static readonly Random RAND = new Random();

        static List<int> GenList(int length, RandomGenType type)
        {
            switch (type)
            {
                case RandomGenType.Normal:
                    return GenNormal(length);
                case RandomGenType.Sorted:
                    return GenSorted(length);
                case RandomGenType.SortedReverse:
                    return GenSortedReverse(length);
                case RandomGenType.NearlySorted:
                    return GenNearlySorted(length);
                case RandomGenType.Mirror:
                    return GenMirror(length);
                default:
                    throw new ArgumentException("Invalid list type");
            }
        }

        static List<int> GenNormal(int length)
        {
            return Enumerable.Range(0, length).Select(x => RAND.Next(MIN, MAX + 1)).ToList();
        }

        static List<int> GenSorted(int length)
        {
            int i = RAND.Next(MIN, MAX - length);
            return Enumerable.Range(i, i + length + 1).ToList();
        }

        static List<int> GenSortedReverse(int length)
        {
            List<int> list = GenSorted(length);
            list.Reverse();
            return list;
        }

        static List<int> GenNearlySorted(int length)
        {
            List<int> list = GenSorted(length);
            for (int i = 0; i < MAX / NEARLY_SORTED_SENSITIVITY; i++)
            {
                int a = RAND.Next(MIN, MAX);
                int b = RAND.Next(MIN, MAX);
                (list[b], list[a]) = (list[a], list[b]);
            }
            return list;
        }

        static List<int> GenMirror(int length)
        {
            List<int> list = new List<int>(new int[length]);
            int limit = length / 2;
            for (int i = 0; i < limit; i++)
            {
                int randomValue = RAND.Next(MIN, MAX + 1);
                list[i] = randomValue;
                list[length - i - 1] = randomValue;
            }
            if (length % 2 != 0)
            {
                list[limit] = RAND.Next(MIN, MAX + 1);
            }
            return list;
        }
    }
}
