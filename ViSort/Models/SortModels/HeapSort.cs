using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;

class HeapSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Heap;
    public static string TimeComplexity => "O(n Log n)";
    public static string SpaceComplexity => "O(n Log n)";
    public static string YoutubeLink => "https://youtu.be/2DmK_H7IdTo?si=cg1oxWLwzJ3l4TMl";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/heap-sort/";

    public override async Task BeginAlgorithm()
    {
        int N = Elements.Count;
        for (int i = (N / 2) - 1; i >= 0; i--)
        {
            Heapify(Elements, N, i);
        }
        for (int i = N - 1; i > 0; i--)
        {
            Step++;
            await DrawRect.SwapElementsAsync(Elements, 0, i);
            Heapify(Elements, i, 0);
        }
    }

    public async void Heapify(List<int> Elements, int N, int i)
    {
        int largest = i;
        int l = (2 * i) + 1;
        int r = (2 * i) + 2;
        if (l < N && Elements[l].CompareTo(Elements[largest]) > 0)
        {
            largest = l;
        }
        if (r < N && Elements[r].CompareTo(Elements[largest]) > 0)
        {
            largest = r;
        }
        if (largest != i)
        {
            Step++;
            await DrawRect.SwapElementsAsync(Elements, i, largest);
            Heapify(Elements, N, largest);
        }
    }
}