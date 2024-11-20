using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Linq;
using ViSort.Draw;
using ViSort.Models;
using static ViSort.Utils.Utils;

namespace ViSort.Models.SortModels;

public class InsertionSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Insertion;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";
    public async override void BeginAlgorithm()
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