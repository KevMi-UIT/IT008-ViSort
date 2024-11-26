using System.Windows.Media;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;
class MergeSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Merge;
    public override string TimeComplexity => "O(nlog(n))";
    public override string SpaceComplexity => "O(n)";
    public override string YoutubeLink => "https://www.youtube.com/watch?v=ZRPoEKHXTJg";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/merge-sort/";

    public override async Task BeginAlgorithm()
    {
        await StartMergeSort(0, Elements.Count - 1);
    }

    public void Merge(int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;
        int[] L = new int[n1];
        int[] R = new int[n2];
        int i, j;
        Step++;
        for (i = 0; i < n1; ++i)
            L[i] = Elements[l + i];
        Step++;
        for (j = 0; j < n2; ++j)
            R[j] = Elements[m + 1 + j];
        i = 0;
        j = 0;
        int k = l;
        Step++;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                Elements[k] = L[i];
                i++;
            }
            else
            {
                Elements[k] = R[j];
                j++;
            }
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Black);
            DrawRect.SetOneRectangleColor(k, Colors.Red);
            k++;
        }
        Step++;
        while (i < n1)
        {
            Elements[k] = L[i];
            i++;
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Black);
            DrawRect.SetOneRectangleColor(k, Colors.Red);
            k++;
        }
        Step++;
        while (j < n2)
        {
            Elements[k] = R[j];
            j++;
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Black);
            DrawRect.SetOneRectangleColor(k, Colors.Red);
            k++;
        }
    }

    public async Task StartMergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            Step++;
            await StartMergeSort(l, m);
            Step++;
            await StartMergeSort(m + 1, r);
            await Task.Delay(DrawRect.ThreadDelay);
            Step++;
            Merge(l, m, r);
        }
    }
}