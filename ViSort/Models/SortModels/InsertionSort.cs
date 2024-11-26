using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;

public class InsertionSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Insertion;
    public override string TimeComplexity => "O(n^2)";
    public override string SpaceComplexity => "O(1)";
    public override string YoutubeLink => "https://youtu.be/mTNC0ERo-ZI?si=BTaxxQLXZPsRooAq";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/insertion-sort-algorithm/";

    public override async Task BeginAlgorithm()
    {
        for (int i = 1; i < Elements.Count; i++)
        {
            int compareIndex = i;

            while (Elements[compareIndex] < Elements[compareIndex - 1])
            {
                Step++;
                await DrawRect.SwapElementsAsync(Elements, compareIndex - 1, compareIndex);
                compareIndex--;
                if (compareIndex == 0)
                {
                    break;
                }
            }
        }
    }
}