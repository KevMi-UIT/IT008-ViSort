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
using Windows.UI.Composition.Interactions;

namespace ViSort.Models.SortModels;

class BubbleSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Bubble;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";

    public async override void BeginAlgorithm()
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