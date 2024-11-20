using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Linq;
using ViSort.Draw;
using ViSort.Models;
using ViSort.Types;
using static ViSort.Utils.Utils;

namespace ViSort.Models.SortModels;

public class InsertionSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Insertion;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/mTNC0ERo-ZI?si=BTaxxQLXZPsRooAq";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/insertion-sort-algorithm/";

    public override async Task BeginAlgorithm()
    {
        for (int i = 1; i < Elements.Count; i++)
        {
            int compareIndex = i;

            while (Elements[compareIndex] < Elements[compareIndex - 1])
            {
                Step++;
                await DrawRect.SwapElementsAsync(Elements, compareIndex - 1, compareIndex);
                compareIndex--;
                if (compareIndex == 0)
                {
                    break;
                }
            }
        }
    }
}