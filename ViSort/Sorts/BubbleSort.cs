using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Visort.Drawing;
using Windows.UI.Composition.Interactions;

namespace ViSort.Sorts;

class BubbleSort : BaseSort
{
    public override int ElementCount { get; set; }

    public async override void BeginAlgorithm()
    {
        for (int i = 0; i < ElementCount; i++)
        {
            for (int j = 0; j < ElementCount - 1; j++)
            {
                if (Elements[j] > Elements[j + 1])
                    await SwapElementsAsync(j, j + 1);
            }
        }
    }
}