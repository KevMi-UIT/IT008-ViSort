using System;
using System.Collections.Generic;
using static ViSort.Utils.Utils;   

namespace ViSort.Sorts
{
    public class BubbleSort : AlgorithmBase
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
                        Swap(list, j, j + 1);
                        swapped = true;
                    }
                }
                if (swapped == false)
                    break;
            }
        }
    }
}
