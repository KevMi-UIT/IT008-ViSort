using System.Windows.Controls;
using System.Windows.Media;
using ViSort.Draw;
using ViSort.Models;
using ViSort.Types;

namespace ViSort.Models.SortModels;

public class RadixSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Radix;
    public static string TimeComplexity => "O(d*(n+b))";
    public static string SpaceComplexity => "O(n+b)";
    public static string YoutubeLink => "https://youtu.be/nu4gDuFabIM?si=EudyVCS6GOlBhLAv";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/radix-sort/";

    public async override Task BeginAlgorithm()
    {
        await StartRadixSort(Elements);
    }

    private async Task StartRadixSort(List<int> Elements)
    {
        // Number of digits in the largest number
        int maxDigits = Elements.Max().ToString().Length;
        Step++;
        // Perform radix sort, one digit at a time
        for (int i = 1; i <= maxDigits; i++)
        {
            await Task.Delay(DrawRect.ThreadDelay); // Pause before sorting the next digit
            Step++;
            CountSort(Elements, i); // Sort based on the current digit
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Red); // Visualize current sorting step
        }

        await Task.Delay(220); // Small pause after sorting completes
        DrawRect.DrawRectangleOnCanvas(Elements, Colors.Black); // Final visualization with sorted array
    }

    private void CountSort(List<int> Elements, int LengthToMinus)
    {
        // Temporary buckets for counting sort based on the current digit
        string[] elementDuplicates = new string[10];
        Step++;
        for (int i = 0; i < Elements.Count; i++)
        {
            string currentElement = Elements[i].ToString();

            // Pad numbers with leading zeros for consistent digit lengths
            while (currentElement.Length < 3)
            {
                currentElement = $"0{currentElement}";
            }

            // Find the digit to sort by (LengthToMinus determines the position)
            int currentIndex = Convert.ToInt32(currentElement[^LengthToMinus].ToString());
            elementDuplicates[currentIndex] += $"{currentElement},";
        }

        int ElementsIndex = 0;

        // Rebuild the elements list in sorted order based on the current digit
        Step++;
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
}