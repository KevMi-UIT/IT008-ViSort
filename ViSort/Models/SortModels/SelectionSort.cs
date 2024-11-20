using System;
using System.Collections.Generic;
using ViSort.Draw;
using static ViSort.Utils.Utils;

namespace ViSort.Models.SortModels;

public class SelectionSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Selection;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";
    public async override void BeginAlgorithm()
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