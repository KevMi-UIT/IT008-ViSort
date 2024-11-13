using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ViSort.Sorts;

namespace ViSort.Sorts
{
    class BubbleSort : Base
    {
        public override int elementCount { get; set; }

        public override void SetComplexity()
        {
            timeComplexity = "O(nLog(n))";
            spaceComplexity = "O(1)";
        }
        public override void BeginAlgorithm(List<int> elements)
        {
            StartBubbleSort(elements);
        }

        private void StartBubbleSort(List<int> elements)
        {
            for (int i = 0; i < elementCount; i++)
            {
                for (int j = 0; j < elementCount - 1; j++)
                {
                    if (elements[j] > elements[j + 1])
                    {
                        SwapElements(j, j + 1, elements, 0);
                    }
                }
            }
        }
    }
}
