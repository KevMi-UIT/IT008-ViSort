using System.Windows.Media;
using ViSort.Draw;
using ViSort.Types;
// Creat DrawOneRectangle funct
//NOT DONE YET

namespace ViSort.Models.SortModels;
class TimSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Tim;
    public override string TimeComplexity => "O(n Log n)";
    public override string SpaceComplexity => "O(n)";
    public override string YoutubeLink => "https://youtu.be/NVIjHj-lrT4?si=Rlpr2UG8qLMs4c9b";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/timsort/";
    public const int Run = 32;

    public void InsertionSort(List<int> list, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int Temp = list[i];
            DrawRect.SetRectangleColor(i, Colors.Red);
            int j = i - 1;
            while (j >= left && list[j].CompareTo(Temp) > 0)
            {
                list[j + 1] = list[j];
                DrawRect.SetRectangleColor(j, Colors.Red);
                DrawRect.SetRectangleColor(j + 1, Colors.Red);
                j--;
            }
            list[j + 1] = Temp;
        }
    }

    public static void Merge(List<int> list, int left, int mid, int right)
    {
        int i, j, k;
        int Length1 = mid - left + 1, Length2 = right - mid;
        int[] LeftArr = new int[Length1];
        int[] RightArr = new int[Length2];
        for (i = 0; i < Length1; i++)
            LeftArr[i] = list[left + i];
        for (i = 0; i < Length2; i++)
            RightArr[i] = list[mid + 1 + i];
        i = 0;
        j = 0;
        k = 0;
        while (i < Length1 && j < Length2)
        {
            if (LeftArr[i].CompareTo(RightArr[j]) <= 0)
            {
                list[k] = LeftArr[i];
                i++;
            }
            else
            {
                list[k] = RightArr[j];
                j++;
            }
            k++;
        }
        while (i < Length1)
        {
            list[k] = LeftArr[i];
            k++;
            i++;
        }
        while (j < Length2)
        {
            list[k] = RightArr[j];
            k++;
            j++;
        }
    }

    public override Task BeginAlgorithm()
    {
        int N = Elements.Count;
        for (int i = 0; i < N; i += Run)
        {
            InsertionSort(Elements, i, Math.Min(i + Run - 1, N - 1));
        }
        for (int size = Run; size < N; size = size * 2)
        {
            for (int left = 0; left < N; left += size * 2)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + (size * 2) - 1, N - 1);
                if (mid < right)
                    Merge(Elements, left, mid, right);
            }
        }

        return Task.CompletedTask;
    }
}