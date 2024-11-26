using System.Diagnostics;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;
class QuickSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Quick;
    public override string TimeComplexity => "O(n Log n)";
    public override string SpaceComplexity => "O(n)";
    public override string YoutubeLink => "https://youtu.be/WprjBK0p6rw?si=G14ohw6siZ3qa7xC";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/quick-sort-algorithm/";

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