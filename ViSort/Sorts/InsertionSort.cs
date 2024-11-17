//using System;
//using System.Collections.Generic;
//using System.Windows.Media;
//using System.Xml.Linq;
//using static ViSort.Utils.Utils;

//namespace ViSort.Sorts;

//public class InsertionSort : BaseSort
//{
//    public override int ElementCount { get; set; }
//    public async override Task BeginAlgorithm()
//    {
//        for (int i = 1; i < ElementCount; i++)
//        {
//            int compareIndex = i;

//            while (Elements[compareIndex] < Elements[compareIndex - 1])
//            {
//               await SwapElementsAsync(compareIndex - 1, compareIndex);

//                compareIndex--;

//                if (compareIndex == 0)
//                {
//                    break;
//                }
//            }
//        }
//    }
//}