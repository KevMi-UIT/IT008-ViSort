using ViSort.Draw;
using ViSort.Models;

namespace ViSort.Models.SortModels;
public class ShellSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Shell;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";
    public async override void BeginAlgorithm()
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
                    //Elements[j] = Elements[j - Increment]; // => SwapElementsAsync
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