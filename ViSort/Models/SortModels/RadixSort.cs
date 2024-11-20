using ViSort.Draw;
using ViSort.Models;
using ViSort.Models.SortModels;

namespace ViSort.Models.SortModels;

public class RadixSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType { get; } = SortTypes.Bubble;
    public override string TimeComplexity { get; } = "";
    public override string SpaceComplexity { get; } = "";
    public static int GetMax(List<int> Elements)
    {
        int N = Elements.Count;
        int M = 0;
        for (int i = 0; i < N; i++)
        {
            M = Math.Max(M, Elements[i]);
        }
        return M;
    }

    public void CountingSort(List<int> Elements, int exp)
    {
        int N = Elements.Count;
        List<int> countElements = new(new int[10]);
        List<int> outElements = new(new int[N]);

        for (int i = 0; i < N; i++)
        {
            countElements[Elements[i] / exp % 10]++;
        }

        for (int i = 1; i < 10; i++)
        {
            countElements[i] += countElements[i - 1];
        }

        for (int i = N - 1; i >= 0; i--)
        {
            outElements[countElements[Elements[i] / exp % 10] - 1] = Elements[i];
            countElements[Elements[i] / exp % 10]--;
        }

        for (int i = 0; i < N; i++)
        {
            Elements[i] = outElements[i];
        }
    }

    public override void BeginAlgorithm()
    {
        int m = GetMax(Elements);
        for (int exp = 1; m / exp > 0; exp *= 10)
            CountingSort(Elements, exp);
    }
}