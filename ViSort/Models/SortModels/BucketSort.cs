using System.Xml.Linq;
using ViSort.Draw;
using ViSort.Types;
using Windows.ApplicationModel.Appointments.DataProvider;

namespace ViSort.Models.SortModels;
public class BucketSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Bucket;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/9I2oOAr2okY?si=GZlYC7Ab1bvFht59";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bubble-sort-algorithm/";

    public override async Task BeginAlgorithm()
    {
        await StartBucketSort();
    }
    public async Task InsertionSort(List<int> elements)
    {
        for (int i = 1; i < elements.Count; i++)
        {
            int compareIndex = i;

            while (elements[compareIndex] < elements[compareIndex - 1])
            {
                Step++;
                await DrawRect.SwapElementsAsync(elements, compareIndex - 1, compareIndex);
                compareIndex--;
                if (compareIndex == 0)
                {
                    break;
                }
            }
        }
    }

    async Task StartBucketSort()
    {
        List<int>[] buckets = new List<int>[Elements.Count];
        for (int i = 0; i < Elements.Count; i++)
        {
            Step++;
            buckets[i] = new List<int>();
        }
        for (int i = 0; i < Elements.Count; i++)
        {
            Step++;
            int bi = Elements.Count * Elements[i];
            buckets[bi].Add(Elements[i]);
        }
        for (int i = 0; i < Elements.Count; i++)
        {
            Step++;
            await InsertionSort(buckets[i]);
        }
        int index = 0;
        for (int i = 0; i < Elements.Count; i++)
        {
            Step++;
            for (int j = 0; j < buckets[i].Count; j++)
            {
                Step++;
                Elements[index++] = buckets[i][j];
            }
        }
    }
}