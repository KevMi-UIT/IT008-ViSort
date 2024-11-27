using System.Windows.Media;
using ViSort.Types;
// Creat DrawOneRectangle funct
//NOT DONE YET

namespace ViSort.Models.SortModels;
class TimSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Tim;
    public override string TimeComplexity => "O(nLog(n))";
    public override string SpaceComplexity => "O(n)";
    public override string YoutubeLink => "https://youtu.be/NVIjHj-lrT4?si=Rlpr2UG8qLMs4c9b";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/timsort/";
    public const int Run = 32;

    public async Task InsertionSortAsync(List<int> list, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            Step++;
            int temp = list[i];
            DrawRect.SetOneRectangleColor(i, Colors.Red);
            await Task.Delay(DrawRect.ThreadDelay);
            int j = i - 1;

            while (j >= left && list[j].CompareTo(temp) > 0)
            {
                Step++;
                list[j + 1] = list[j];
                DrawRect.SetOneRectangleColor(j, Colors.Yellow);
                await Task.Delay(DrawRect.ThreadDelay);
                DrawRect.DrawRectangleOnCanvas(list, Colors.Gray);
                j--;
            }

            list[j + 1] = temp;
            DrawRect.DrawRectangleOnCanvas(list, Colors.Gray);
            DrawRect.SetOneRectangleColor(i, Colors.Gray);
        }
    }

    public async Task MergeAsync(List<int> list, int left, int mid, int right)
    {
        int i, j, k;
        int length1 = mid - left + 1, length2 = right - mid;
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

            DrawRect.SetOneRectangleColor(k, Colors.Green);
            DrawRect.DrawRectangleOnCanvas(list, Colors.Gray);
            await Task.Delay(DrawRect.ThreadDelay);
            k++;
        }

        while (i < length1)
        {
            Step++;
            list[k] = leftArr[i];
            DrawRect.SetOneRectangleColor(k, Colors.Blue);
            await Task.Delay(DrawRect.ThreadDelay);
            DrawRect.DrawRectangleOnCanvas(list, Colors.Gray);
            i++;
            k++;
        }
        Step++;
        while (j < length2)
        {
            list[k] = rightArr[j];
            DrawRect.SetOneRectangleColor(k, Colors.Blue);
            await Task.Delay(DrawRect.ThreadDelay);
            DrawRect.DrawRectangleOnCanvas(list, Colors.Gray);
            j++;
            k++;
        }
    }

    public override async Task BeginAlgorithmAsync()
    {
        int n = Elements.Count;
        for (int i = 0; i < n; i += Run)
        {
            await InsertionSortAsync(Elements, i, Math.Min(i + Run - 1, n - 1));
        }
        for (int size = Run; size < n; size *= 2)
        {
            for (int left = 0; left < n; left += size * 2)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + (size * 2) - 1, n - 1);

                if (mid < right)
                {
                    await MergeAsync(Elements, left, mid, right);
                }
            }
        }
    }
}