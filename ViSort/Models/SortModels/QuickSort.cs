using System.Diagnostics;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;
class QuickSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Quick;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/9I2oOAr2okY?si=GZlYC7Ab1bvFht59";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bubble-sort-algorithm/";

    protected int IntPartiton { get; set; }
    public override async Task BeginAlgorithm()
    {
        await Recursive(0, Elements.Count - 1);
    }
    private async Task Partition(int low, int high)
    {
        int pivot = Elements[high];
        int i = low - 1;
        for (int j = low; j <= high - 1; j++)
        {
            if (Elements[j] < pivot)
            {
                i++;
                Step++;
                await Task.Delay(DrawRect.ThreadDelay);
                await DrawRect.SwapElementsAsync(Elements, i, j);
            }
        }
        Step++;
        await Task.Delay(DrawRect.ThreadDelay);
        await DrawRect.SwapElementsAsync(Elements, i + 1, high);
        IntPartiton = i + 1;
    }

    public async Task Recursive(int low, int high)
    {
        if (low < high)
        {
            await Partition(low, high);
            int pi = IntPartiton;
            await Recursive(low, pi - 1);
            await Recursive(pi + 1, high);
        }
    }
}