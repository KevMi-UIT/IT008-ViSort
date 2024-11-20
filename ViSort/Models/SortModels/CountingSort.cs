using System.Windows.Media;
using System.Windows.Shapes;
using ViSort.Draw;
using Windows.Devices.PointOfService;
using Windows.Foundation.Metadata;

namespace ViSort.Models.SortModels;
public class CountingSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Counting;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";

    public async override Task BeginAlgorithm()
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