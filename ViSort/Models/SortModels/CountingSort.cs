using System.Windows.Media;
using ViSort.Types;

namespace ViSort.Models.SortModels;
public class CountingSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Counting;
    public override string TimeComplexity => "O(m+n)";
    public override string SpaceComplexity => "O(m+n)";
    public override string YoutubeLink => "https://youtu.be/EItdcGhSLf4?si=BLxR9NVqKJKjXoKO";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/counting-sort/";

    public override async Task BeginAlgorithmAsync()
    {
        int N = Elements.Count;
        Step++;
        int M = 0;
        for (int i = 0; i < N; i++)
        {
            M = Math.Max(M, Elements[i]);
            DrawRect.SetOneRectangleColor(i, Colors.Red);
            await Task.Delay((int)DrawRect.ThreadDelay / 10);
        }
        List<int> CountArray = new(new int[M + 1]);
        Step++;
        for (int i = 0; i < N; i++)
        {
            CountArray[Elements[i]]++;
        }
        Step++;
        for (int i = 1; i <= M; i++)
        {
            CountArray[i] += CountArray[i - 1];
            await Task.Delay(DrawRect.ThreadDelay);
            DrawRect.DrawRectangleOnCanvas(CountArray, Colors.Black);
        }
        Step++;
        List<int> OutputArray = new(new int[N]);
        for (int i = N - 1; i >= 0; i--)
        {
            OutputArray[CountArray[Elements[i]] - 1] = Elements[i];
            CountArray[Elements[i]]--;
            DrawRect.DrawRectangleOnCanvas(OutputArray, Colors.Green);
            await Task.Delay(DrawRect.ThreadDelay);
        }
        Step++;
        Elements.Clear();
        Elements.AddRange(OutputArray);
    }
}