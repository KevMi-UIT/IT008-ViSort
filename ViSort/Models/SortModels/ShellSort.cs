using ViSort.Types;

namespace ViSort.Models.SortModels;
public class ShellSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Shell;
    public override string TimeComplexity => "O(n^2)";
    public override string SpaceComplexity => "O(1)";
    public override string YoutubeLink => "https://youtu.be/SHcPqUe2GZM?si=61x1MQdsPuX8K345";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/shell-sort/";
    public override async Task BeginAlgorithmAsync()
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