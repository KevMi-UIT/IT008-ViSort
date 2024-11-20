using System.Diagnostics;
using ViSort.Draw;

namespace ViSort.Models.SortModels;
class QuickSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Quick;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";

    protected int IntPartiton { get; set; }
    public async override Task BeginAlgorithm()
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