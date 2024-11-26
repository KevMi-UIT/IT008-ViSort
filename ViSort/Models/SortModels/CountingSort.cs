using System.Windows.Media;
using System.Windows.Shapes;
using ViSort.Draw;
using ViSort.Types;
using Windows.Devices.PointOfService;
using Windows.Foundation.Metadata;

namespace ViSort.Models.SortModels;
public class CountingSort : SortModel, ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Counting;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://www.youtube.com/watch?v=xFPi5BWI06k";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/counting-sort//";

    public CountingSort(List<int> _element, DrawRectangle _drawRectangle) : base(_element.Select(x => x / 10).ToList(), _drawRectangle)
    {
        _drawRectangle.MaxValueOfElement = _drawRectangle.MaxValueOfElement / 10;
    }

    public override async Task BeginAlgorithm()
    {
        int N = Elements.Count;

        // Step 1: Find the maximum value in the array
        Step++;
        int M = 0; // Max value
        for (int i = 0; i < N; i++)
        {
            M = Math.Max(M, Elements[i]);
            DrawRect.SetOneRectangleColor(i, Colors.Red); // Highlight the current element
            await Task.Delay(DrawRect.ThreadDelay / 10);
        }
        // Create a count array of size M+1
        List<int> CountArray = new(new int[M + 1]);

        // Step 2: Count occurrences of each element
        Step++;
        for (int i = 0; i < N; i++)
        {
            CountArray[Elements[i]]++;
            // DrawRect.DrawRectangleOnCanvas(CountArray, Colors.Blue); // Visualize count array
        }

        // Step 3: Update count array to store cumulative sums
        Step++;
        for (int i = 1; i <= M; i++)
        {
            CountArray[i] += CountArray[i - 1];
            DrawRect.DrawRectangleOnCanvas(CountArray, Colors.Purple); // Visualize cumulative sums
        }

        // Step 4: Construct the output array
        Step++;
        List<int> OutputArray = new(new int[N]);
        for (int i = N - 1; i >= 0; i--)
        {
            OutputArray[CountArray[Elements[i]] - 1] = Elements[i];
            CountArray[Elements[i]]--;
            DrawRect.DrawRectangleOnCanvas(OutputArray, Colors.Blue); // Visualize output array
            await Task.Delay(DrawRect.ThreadDelay / 10);
        }

        // Step 5: Update the original array with the sorted values
        Step++;
        Elements.Clear();
        Elements.AddRange(OutputArray);
    }
}