using System.Windows.Media;
using ViSort.Types;

namespace ViSort.Models.SortModels;

public class RadixSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Radix;
    public override string TimeComplexity => "O(d*(n+b))";
    public override string SpaceComplexity => "O(n+b)";
    public override string YoutubeLink => "https://youtu.be/nu4gDuFabIM?si=EudyVCS6GOlBhLAv";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/radix-sort/";
    public override async Task BeginAlgorithmAsync()
    {
        await StartRadixSort(Elements);
    }
    private async Task StartRadixSort(List<int> Elements)
    {
        int maxDigits = Elements.Max().ToString().Length;
        Step++;
        for (int i = 1; i <= maxDigits; i++)
        {
            await Task.Delay(DrawRect.ThreadDelay * 10);
            Step++;
            CountSort(Elements, i);
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Red);
        }
        await Task.Delay(DrawRect.ThreadDelay + 100);
        DrawRect.DrawRectangleOnCanvas(Elements, Colors.Black);
    }

    private void CountSort(List<int> Elements, int LengthToMinus)
    {
        string[] elementDuplicates = new string[10];
        Step++;
        for (int i = 0; i < Elements.Count; i++)
        {
            string currentElement = Elements[i].ToString();

            while (currentElement.Length < 3)
            {
                currentElement = $"0{currentElement}";
            }
            int currentIndex = Convert.ToInt32(currentElement[^LengthToMinus].ToString());
            elementDuplicates[currentIndex] += $"{currentElement},";
        }
        int ElementsIndex = 0;
        Step++;
        for (int i = 0; i < 10; i++)
        {
            if (elementDuplicates[i] != null)
            {
                Step++;
                string[] split = elementDuplicates[i].Split(',');
                for (int j = 0; j < split.Length - 1; j++)
                {
                    Step++;
                    Elements[ElementsIndex] = Convert.ToInt32(split[j]);
                    ElementsIndex++;
                }
            }
        }
    }
}