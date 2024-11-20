using ViSort.Draw;
using ViSort.Models;
using ViSort.Types;

namespace ViSort.Models.SortModels;
public class ShellSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    public static SortTypes SortType => SortTypes.Shell;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/SHcPqUe2GZM?si=61x1MQdsPuX8K345";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/shell-sort/";
    public override async Task BeginAlgorithm()
    {
        int N = Elements.Count;
        int i, j;
        int Increment = 3;
        int temp;
        while (Increment > 0)
        {
            for (i = 0; i < N; i++)
            {
                j = i;
                temp = Elements[i];
                while (j >= Increment && Elements[j - Increment].CompareTo(temp) > 0)
                {
                    Step++;
                    await DrawRect.SwapElementsAsync(Elements, j, j - Increment);
                    j -= Increment;
                }
                Elements[j] = temp;
            }
            if (Increment / 2 != 0)
                Increment /= 2;
            else if (Increment == 1)
                Increment = 0;
            else
                Increment = 1;
        }
    }
}