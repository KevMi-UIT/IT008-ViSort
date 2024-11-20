using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;

class BubbleSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Bubble;
    public override string TimeComplexity => "O(n^2)";
    public override string SpaceComplexity => "O(1)";
    public override string YoutubeLink => "https://youtu.be/9I2oOAr2okY?si=GZlYC7Ab1bvFht59";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bubble-sort-algorithm/";

    public override async Task BeginAlgorithm()
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            for (int j = 0; j < Elements.Count - 1; j++)
            {
                if (Elements[j] > Elements[j + 1])
                {
                    Step++;
                    await DrawRect.SwapElementsAsync(Elements, j, j + 1);
                }
            }
        }
    }
}