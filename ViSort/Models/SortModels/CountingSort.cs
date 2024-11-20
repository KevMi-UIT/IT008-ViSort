using System.Windows.Media;
using System.Windows.Shapes;
using ViSort.Draw;
using ViSort.Types;
using Windows.Devices.PointOfService;
using Windows.Foundation.Metadata;

namespace ViSort.Models.SortModels;
public class CountingSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Counting;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/9I2oOAr2okY?si=GZlYC7Ab1bvFht59";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bubble-sort-algorithm/";

    public override async Task BeginAlgorithm()
    {
        int N = Elements.Count;
        int M = 0;
        for (int i = 0; i < N; i++)
        {
            Step++;
            M = Math.Max(M, Elements[i]);
            DrawRect.SetRectangleColor(M, Colors.Red);
            await Task.Delay(DrawRect.ThreadDelay);
        }
        List<int> CountArray = new(new int[M + 1]);
        for (int i = 0; i < N; i++)
        {
            Step++;
            CountArray[Elements[i]]++;
        }
        DrawRect.DrawCanvas.Children.Clear();
        DrawRect.BrushColor = Colors.Green;
        DrawRect.DrawRectangleOnCanvas(CountArray);
        for (int i = 1; i <= M; i++)
        {
            Step++;
            CountArray[i] += CountArray[i - 1];
        }
        DrawRect.DrawCanvas.Children.Clear();
        DrawRect.BrushColor = Colors.Green;
        DrawRect.DrawRectangleOnCanvas(CountArray);
        List<int> outputArray = new(new int[N]);
        for (int i = N - 1; i >= 0; i--)
        {
            Step++;
            outputArray[CountArray[Elements[i]] - 1] = Elements[i];
            CountArray[Elements[i]]--;
        }
        Elements = new List<int>(outputArray);
    }
}