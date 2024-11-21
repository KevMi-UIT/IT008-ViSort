using ViSort.Draw;
using ViSort.Models;
using ViSort.Models.SortModels;
using ViSort.Types;
using static ViSort.Exceptions.SortExceptions;

namespace ViSort.Utils;

public static class SortUtils
{
    public static SortModel InstantiateSort(SortTypes sortType, List<int> elements, DrawRectangle drawRectangle)
    {
        // TODO: Readd Merge sort
        return sortType switch
        {
            SortTypes.Bubble => new BubbleSort(elements, drawRectangle),
            SortTypes.Bucket => new BucketSort(elements, drawRectangle),
            SortTypes.Counting => new CountingSort(elements, drawRectangle),
            SortTypes.Heap => new HeapSort(elements, drawRectangle),
            SortTypes.Insertion => new InsertionSort(elements, drawRectangle),
            // SortTypes.Merge => new MergeSort(elements, drawRectangle),
            SortTypes.Quick => new QuickSort(elements, drawRectangle),
            SortTypes.Radix => new RadixSort(elements, drawRectangle),
            SortTypes.Selection => new SelectionSort(elements, drawRectangle),
            SortTypes.Shell => new ShellSort(elements, drawRectangle),
            SortTypes.Tim => new TimSort(elements, drawRectangle),
            SortTypes.Tree => new TreeSort(elements, drawRectangle),
            _ => throw new SortNotImplemented()
        };
    }

    public static SortTypes GetSortType(string sortType)
    {
        return sortType switch
        {
            "Bubble Sort" => SortTypes.Bubble,
            "Bucket Sort" => SortTypes.Bucket,
            "Counting Sort" => SortTypes.Counting,
            "Heap Sort" => SortTypes.Heap,
            "Insertion Sort" => SortTypes.Insertion,
            "Merge Sort" => SortTypes.Merge,
            "Quick Sort" => SortTypes.Quick,
            "Radix Sort" => SortTypes.Radix,
            "Selection Sort" => SortTypes.Selection,
            "Shell Sort" => SortTypes.Shell,
            "Tim Sort" => SortTypes.Tim,
            _ => throw new SortUdefined()
        };
    }

    public static string GetSortName(SortTypes sortType)
    {
        return sortType switch
        {
            SortTypes.Bubble => "Bubble Sort",
            SortTypes.Bucket => "Bucket Sort",
            SortTypes.Counting => "Counting Sort",
            SortTypes.Selection => "Selection Sort",
            SortTypes.Insertion => "Insertion Sort",
            SortTypes.Merge => "Merge Sort",
            SortTypes.Quick => "Quick Sort",
            SortTypes.Heap => "Heap Sort",
            SortTypes.Radix => "Radix Sort",
            SortTypes.Shell => "Shell Sort",
            SortTypes.Tim => "Tim Sort",
            SortTypes.Tree => "Tree Sort",
            _ => throw new SortUdefined()
        };
    }
}