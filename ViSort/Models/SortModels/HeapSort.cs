using System.Windows.Controls;
using System.Windows.Media;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;

class HeapSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Heap;
    public static string TimeComplexity => "O(n Log n)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://www.youtube.com/watch?v=MtQL_ll5KhQ";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/heap-sort/";

    public override async Task BeginAlgorithm()
    {
        int n = Elements.Count;

        // Build the max heap
        for (int i = (n / 2) - 1; i >= 0; i--)
        {
            await HeapifyAsync(Elements, n, i);
        }

        // Extract elements one by one from the heap
        for (int i = n - 1; i > 0; i--)
        {
            Step++;
            await DrawRect.SwapElementsAsync(Elements, 0, i); // Move current root to the end
            await HeapifyAsync(Elements, i, 0); // Call heapify on the reduced heap
        }
    }

    public async Task HeapifyAsync(List<int> elements, int n, int i)
    {
        int largest = i; // Initialize largest as root
        int l = (2 * i) + 1; // Left child index
        int r = (2 * i) + 2; // Right child index

        // Check if left child exists and is larger than root
        if (l < n && elements[l].CompareTo(elements[largest]) > 0)
        {
            largest = l;
        }

        // Check if right child exists and is larger than largest so far
        if (r < n && elements[r].CompareTo(elements[largest]) > 0)
        {
            largest = r;
        }

        // If the largest is not root, swap and continue heapifying
        if (largest != i)
        {
            Step++;
            await DrawRect.SwapElementsAsync(elements, i, largest); // Swap root with largest
            await HeapifyAsync(elements, n, largest); // Recursively heapify the affected subtree
        }

        // Visualize the current state of the heap
        DrawRect.DrawRectangleOnCanvas(elements, Colors.Black);
        await Task.Delay(DrawRect.ThreadDelay);
    }
}