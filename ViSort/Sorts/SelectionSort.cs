//using System;
//using System.Collections.Generic;
//using static ViSort.Utils.Utils;

//namespace ViSort.Sorts;

//public class SelectionSort : BaseSort
//{
//    public override int ElementCount { get; set; }

//    public async override Task BeginAlgorithm()
//    {
//        for (int i = 0; i < ElementCount; i++)
//        {
//            int minIndex = i;
//            for (int j = i + 1; j < ElementCount; j++)
//            {
//                if (Elements[j] < Elements[minIndex])
//                {
//                    minIndex = j;
//                }
//            }
//            if (minIndex != i)
//            {
//                await SwapElementsAsync(i, minIndex);
//            }
//        }
//    }
//}