using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViSort.Draw;
using ViSort.Models;
using ViSort.Models.SortModels;
using Windows.Devices.Radios;

namespace ViSort.Types;
public static class SortTypesExtensions
{
    public static SortModel? CreateSortInstance(this SortTypes sortType, List<int> elements, Canvas canvas)
    {
        var drawRectangle = new DrawRectangle(canvas, 0);
        return sortType switch
        {
            SortTypes.Bubble => new BubbleSort(elements, drawRectangle),
            SortTypes.Bucket => new BucketSort(elements, drawRectangle),
            SortTypes.Counting => new CountingSort(elements, drawRectangle),
            SortTypes.Selection => new SelectionSort(elements, drawRectangle),
            SortTypes.Insertion => new InsertionSort(elements, drawRectangle),
            SortTypes.Merge => new MergeSort(elements, drawRectangle),
            SortTypes.Quick => new QuickSort(elements, drawRectangle),
            SortTypes.Heap => new HeapSort(elements, drawRectangle),
            SortTypes.Radix => new RadixSort(elements, drawRectangle),
            SortTypes.Shell => new ShellSort(elements, drawRectangle),
            SortTypes.Tim => new TimSort(elements, drawRectangle),
            SortTypes.Tree => new TreeSort(elements, drawRectangle),
            _ => null,
        };
    }
}