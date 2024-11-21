using System;
using System.Collections.Generic;
using ViSort.Draw;
using ViSort.Types;
using static ViSort.Utils.Utils;

namespace ViSort.Models.SortModels;

public class SelectionSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Selection;
    public override string TimeComplexity => "O(n^2)";
    public override string SpaceComplexity => "O(1)";
    public override string YoutubeLink => "https://youtu.be/g-PGLbMth_g?si=z3fP6qlKVSTejlXJ";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/selection-sort-algorithm-2/";
    public override async Task BeginAlgorithm()
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < Elements.Count; j++)
            {
                if (Elements[j] < Elements[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                Step++;
                await DrawRect.SwapElementsAsync(Elements, i, minIndex);
            }
        }
    }
}