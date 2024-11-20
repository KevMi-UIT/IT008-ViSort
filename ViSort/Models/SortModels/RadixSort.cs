using ViSort.Draw;
using ViSort.Models;
using ViSort.Types;
//NOT DONE YET
namespace ViSort.Models.SortModels;

public class RadixSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Radix;
    public static string TimeComplexity => "O(d*(n+b))";
    public static string SpaceComplexity => "O(n+b)";
    public static string YoutubeLink => "https://youtu.be/nu4gDuFabIM?si=EudyVCS6GOlBhLAv";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/radix-sort/";
    private string[]? elementDuplicates;
    private async Task StartRadixSort(List<int> Elements)
    {
        for (int i = 1; i < 4; i++)
        {
            await Task.Delay(700);
            CountSort(Elements, i);
            await ReDrawDisplay(Elements);
        }

        await Task.Delay(220);
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

    private async Task ReDrawDisplay(List<int> Elements)
    {
        ClearDisplay();
        for (int i = 0; i < Elements.Count; i++)
        {
            DrawRect.DrawRectangleOnCanvas(Elements);
            await Task.Delay(DrawRect.ThreadDelay);
        }
    }
    public async override Task BeginAlgorithm()
    {
        await StartRadixSort(Elements);
    }
}