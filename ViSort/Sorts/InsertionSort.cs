using System;
using System.Collections.Generic;
using static ViSort.Utils.Utils;

namespace ViSort.Sorts
{
    public class InsertionSort : AlgorithmBase
    {
        public override int elementCount { get; set; }
        public override void SetComplexity()
        {
            timeComplexity = "O(n^2)";
            spaceComplexity = "O(1)";
        }
        public override void BeginAlgorithm(List<int> list)
        {
            Sort(list);
        }
        public static void Sort(List<int> list)
        {
            int N = list.Count;
            for (int i = 1; i < N; ++i)
            {
                int Key = list[i];
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
}