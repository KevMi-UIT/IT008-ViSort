using System.Windows.Media;
using ViSort.Types;

namespace ViSort.Models.SortModels;

class HeapSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Heap;
    public override string TimeComplexity => "O(nLog(n))";
    public override string SpaceComplexity => "O(nLog(n))";
    public override string YoutubeLink => "https://youtu.be/2DmK_H7IdTo?si=cg1oxWLwzJ3l4TMl";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/heap-sort/";

    public override async Task BeginAlgorithmAsync()
    {
        int n = Elements.Count;
        for (int i = (n / 2) - 1; i >= 0; i--)
        {
            await HeapifyAsync(Elements, n, i);
        }
        for (int i = n - 1; i > 0; i--)
        {
            Step++;
            await DrawRect.SwapElementsAsync(Elements, 0, i);
            await HeapifyAsync(Elements, i, 0);
        }
    }

    public async Task HeapifyAsync(List<int> elements, int n, int i)
    {
        int largest = i;
        int l = (2 * i) + 1;
        int r = (2 * i) + 2;
        if (l < n && elements[l].CompareTo(elements[largest]) > 0)
        {
            largest = l;
        }
        if (r < n && elements[r].CompareTo(elements[largest]) > 0)
        {
            largest = r;
        }
        if (largest != i)
        {
            Step++;
            await DrawRect.SwapElementsAsync(elements, i, largest);
            await HeapifyAsync(elements, n, largest);
        }
        DrawRect.DrawRectangleOnCanvas(elements, Colors.Gray);
        await Task.Delay(DrawRect.ThreadDelay);
    }
}