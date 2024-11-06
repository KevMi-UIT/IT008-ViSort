using System;
using System.Collections.Generic;
using static ViSort.Utils.Utils;

namespace ViSort.Sorts
{
    public class SelectionSort : AlgorithmBase
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
                    Swap(list, i, min_idx);
                }
            }
        }
    }
}
