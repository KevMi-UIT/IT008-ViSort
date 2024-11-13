using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ViSort.Sorts
{
    public abstract class Base
    {
        public string timeComplexity;
        public string spaceComplexity;
        public Canvas Canvas { get; set; } // Canvas trong WPF để vẽ
        public int maxWidth;
        public int maxHeight;
        public int threadDelay;

        public abstract int elementCount { get; set; }
        public abstract void BeginAlgorithm(List<int> elements);
        public abstract void SetComplexity();

        public async void ShowCompletedDisplay(List<int> elements)
        {
            await Task.Run(() => ShowAllElementsBlue(elements));
        }

        private void ShowAllElementsBlue(List<int> elements)
        {
            threadDelay = threadDelay == 200 ? 80 : threadDelay == 0 ? 1 : threadDelay;

            for (int i = 0; i < elements.Count(); i++)
            {
                DrawRectangle(i, elements[i], Colors.SkyBlue);
                Thread.Sleep(threadDelay);
            }
        }

        protected void SwapElements(int index1, int index2, List<int> elements, int sortType)
        {
            Canvas.Dispatcher.Invoke(() =>
            {
                SetRectangleColor(index1, Colors.DarkRed);
                SetRectangleColor(index2, Colors.DarkRed);
            });

            Thread.Sleep(threadDelay);

            int tempValue = elements[index1];
            elements[index1] = elements[index2];
            elements[index2] = tempValue;

            Canvas.Dispatcher.Invoke(() =>
            {
                SetRectangleColor(index1, Colors.Black);
                SetRectangleColor(index2, Colors.Black);
            });
        }

        private void DrawRectangle(int index, int height, Color color)
        {
            Rectangle rect = new Rectangle
            {
                Width = maxWidth,
                Height = height,
                Fill = new SolidColorBrush(color)
            };

            Canvas.SetLeft(rect, index * maxWidth);
            Canvas.SetTop(rect, maxHeight - height);

            Canvas.Dispatcher.Invoke(() => Canvas.Children.Add(rect));
        }

        private void SetRectangleColor(int index, Color color)
        {
            if (Canvas.Children[index] is Rectangle rect)
            {
                rect.Fill = new SolidColorBrush(color);
            }
        }
    }
}
