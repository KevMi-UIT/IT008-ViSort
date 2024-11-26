using System.Windows.Media;
using ViSort.Draw;
using ViSort.Types;
// Creat DrawOneRectangle funct
//NOT DONE YET

namespace ViSort.Models.SortModels;
class TimSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Tim;
    public static string TimeComplexity => "O(nlog(n)";
    public static string SpaceComplexity => "O(n)";
    public static string YoutubeLink => "https://www.youtube.com/watch?v=Pxny_TtOnDo";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/timsort/";
    public const int Run = 32;

    public async Task InsertionSortAsync(List<int> list, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            Step++;
            int temp = list[i];
            DrawRect.SetOneRectangleColor(i, Colors.Red); // Highlight current element
            await Task.Delay(DrawRect.ThreadDelay);
            int j = i - 1;

            while (j >= left && list[j].CompareTo(temp) > 0)
            {
                Step++;
                list[j + 1] = list[j];
                DrawRect.SetOneRectangleColor(j, Colors.Yellow); // Highlight comparison
                await Task.Delay(DrawRect.ThreadDelay);
                DrawRect.DrawRectangleOnCanvas(list, Colors.Black);
                j--;
            }

            list[j + 1] = temp;
            DrawRect.DrawRectangleOnCanvas(list, Colors.Black); // Update visualization
            DrawRect.SetOneRectangleColor(i, Colors.Black); // Reset color
        }
    }

    public async Task MergeAsync(List<int> list, int left, int mid, int right)
    {
        int i, j, k;
        int length1 = mid - left + 1, length2 = right - mid;

        // Temporary arrays
        int[] leftArr = new int[length1];
        int[] rightArr = new int[length2];
        Step++;
        for (i = 0; i < length1; i++)
            leftArr[i] = list[left + i];
        Step++;
        for (i = 0; i < length2; i++)
            rightArr[i] = list[mid + 1 + i];

        i = j = 0;
        k = left;

        // Merge two halves
        while (i < length1 && j < length2)
        {
            Step++;
            if (leftArr[i].CompareTo(rightArr[j]) <= 0)
            {
                list[k] = leftArr[i];
                i++;
            }
            else
            {
                list[k] = rightArr[j];
                j++;
            }

            DrawRect.SetOneRectangleColor(k, Colors.Green); // Highlight merged position
            DrawRect.DrawRectangleOnCanvas(list, Colors.Black); // Update canvas
            await Task.Delay(DrawRect.ThreadDelay);
            k++;
        }

        // Copy remaining elements
        while (i < length1)
        {
            Step++;
            list[k] = leftArr[i];
            DrawRect.SetOneRectangleColor(k, Colors.Blue); // Highlight remaining
            await Task.Delay(DrawRect.ThreadDelay);
            DrawRect.DrawRectangleOnCanvas(list, Colors.Black);
            i++;
            k++;
        }
        Step++;
        while (j < length2)
        {
            list[k] = rightArr[j];
            DrawRect.SetOneRectangleColor(k, Colors.Blue); // Highlight remaining
            await Task.Delay(DrawRect.ThreadDelay);
            DrawRect.DrawRectangleOnCanvas(list, Colors.Black);
            j++;
            k++;
        }
    }

    public override async Task BeginAlgorithm()
    {
        int n = Elements.Count;

        // Sort subarrays of size `Run` using Insertion Sort
        for (int i = 0; i < n; i += Run)
        {
            await InsertionSortAsync(Elements, i, Math.Min(i + Run - 1, n - 1));
        }

        // Merge sorted subarrays
        for (int size = Run; size < n; size *= 2)
        {
            for (int left = 0; left < n; left += size * 2)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + size * 2 - 1, n - 1);

                if (mid < right)
                {
                    await MergeAsync(Elements, left, mid, right);
                }
            }
        }
    }
}