using ViSort.Draw;
using ViSort.Models;
using ViSort.Models.SortModels;

namespace ViSort.Models.SortModels;

public class RadixSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Bubble;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";
    private string[]? elementDuplicates;
    private void StartRadixSort(List<int> Elements)
    {
        for (int i = 1; i < 4; i++)
        {
            Thread.Sleep(700);

            CountSort(Elements, i);

            //Redraw all Elements
            ReDrawDisplay(Elements);
        }

        Thread.Sleep(220);
    }

    private void CountSort(List<int> Elements, int LengthToMinus)
    {
        elementDuplicates = new string[10];

        for (int i = 0; i < Elements.Count; i++)
        {
            string currentElement = Elements[i].ToString();

            switch (currentElement.Length)
            {
                case 2:
                    currentElement = $"0{currentElement}";
                    break;
                case 1:
                    currentElement = $"00{currentElement}";
                    break;
            }

            int currentIndex = Convert.ToInt32(currentElement[currentElement.Length - LengthToMinus].ToString());
            elementDuplicates[currentIndex] = elementDuplicates[currentIndex] + $"{currentElement},";
        }

        int ElementsIndex = 0;

        for (int i = 0; i < 10; i++)
        {
            if (elementDuplicates[i] != null)
            {
                string[] split = elementDuplicates[i].Split(',');

                for (int j = 0; j < split.Length - 1; j++)
                {
                    Elements[ElementsIndex] = Convert.ToInt32(split[j]);
                    ElementsIndex++;
                }
            }
        }
    }

    private void ClearDisplay()
    {
        DrawRect.DrawCanvas.Children.Clear();
    }

    private void ReDrawDisplay(List<int> Elements)
    {
        ClearDisplay();
        for (int i = 0; i < Elements.Count; i++)
        {
            DrawRect.DrawRectangleOnCanvas(Elements);
            Thread.Sleep(DrawRect.ThreadDelay);
        }
    }
    public override void BeginAlgorithm()
    {
        StartRadixSort(Elements);
    }
    //public static int GetMax(List<int> Elements)
    //{
    //    int N = Elements.Count;
    //    int M = 0;
    //    for (int i = 0; i < N; i++)
    //    {
    //        M = Math.Max(M, Elements[i]);
    //    }
    //    return M;
    //}

    //public void CountingSort(List<int> Elements, int exp)
    //{
    //    int N = Elements.Count;
    //    List<int> countElements = new(new int[10]);
    //    List<int> outElements = new(new int[N]);

    //    for (int i = 0; i < N; i++)
    //    {
    //        countElements[Elements[i] / exp % 10]++;
    //    }

    //    for (int i = 1; i < 10; i++)
    //    {
    //        countElements[i] += countElements[i - 1];
    //    }

    //    for (int i = N - 1; i >= 0; i--)
    //    {
    //        outElements[countElements[Elements[i] / exp % 10] - 1] = Elements[i];
    //        countElements[Elements[i] / exp % 10]--;
    //    }

    //    for (int i = 0; i < N; i++)
    //    {
    //        Elements[i] = outElements[i];
    //    }
    //}
}