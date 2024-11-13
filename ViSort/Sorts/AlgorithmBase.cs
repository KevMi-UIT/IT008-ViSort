using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using SystemColors = System.Drawing.SystemColors;
using static ViSort.Utils.GenRandomList;

namespace ViSort.Sorts
{
    public abstract class AlgorithmBase
    {
        public string timeComplexity;
        public string spaceComplexity;
        public Graphics graphics;
        public int maxWidth;
        public int maxHeight;
        public int threadDelay;

        public abstract int elementCount { get; set; }
        public abstract void BeginAlgorithm(List<int> list);
        public abstract void SetComplexity();
        public async void ShowCompletedDisplay(int[] elements)
        {
            await Task.Run(() => ShowAllElementsBlue(elements));
        }

        private void ShowAllElementsBlue(int[] elements)
        {
            if (threadDelay == 200)
            {
                threadDelay = 80;
            }
            else if (threadDelay == 0)
            {
                threadDelay = 1;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(83, 153, 182)), i * maxWidth, maxHeight - elements[i], maxWidth, elements[i]);
                Thread.Sleep(threadDelay);
            }
        }
      
        protected void SwapElements(int index1, int index2, int[] elements, int sortType)
        {
            switch (sortType)
            {
                case 0:
                    graphics.FillRectangle(new SolidBrush(System.Drawing.Color.DarkRed), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    break;
                case 1:
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);
                    break;
                case 2:
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    graphics.FillRectangle(new SolidBrush(Color.Black), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);
                    break;
                case 3:
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);
                    break;
            }

            Thread.Sleep(threadDelay);

            graphics.FillRectangle(new SolidBrush(SystemColors.ActiveBorder), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
            graphics.FillRectangle(new SolidBrush(System.Drawing.SystemColors.ActiveBorder), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);

            int tempValue = elements[index1]; //Swaps the elements
            elements[index1] = elements[index2];
            elements[index2] = tempValue;

            switch (sortType)
            {
                default:
                    graphics.FillRectangle(new SolidBrush(Color.Black), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    graphics.FillRectangle(new SolidBrush(Color.Black), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);
                    break;
                case 1:
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    graphics.FillRectangle(new SolidBrush(Color.Black), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);

                    if (GetType().Name.Contains("Selection"))
                    {
                        Thread.Sleep(threadDelay);
                    }

                    graphics.FillRectangle(new SolidBrush(Color.Black), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    break;
                case 2:
                    graphics.FillRectangle(new SolidBrush(Color.Black), index1 * maxWidth, maxHeight - elements[index1], maxWidth, elements[index1]);
                    graphics.FillRectangle(new SolidBrush(Color.DarkRed), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);

                    Thread.Sleep(threadDelay);

                    graphics.FillRectangle(new SolidBrush(Color.Black), index2 * maxWidth, maxHeight - elements[index2], maxWidth, elements[index2]);
                    break;
            }
        }
    }
}
