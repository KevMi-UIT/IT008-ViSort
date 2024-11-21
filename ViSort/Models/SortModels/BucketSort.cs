using System.Xml.Linq;
using ViSort.Draw;
using ViSort.Types;
using Windows.ApplicationModel.Appointments.DataProvider;

namespace ViSort.Models.SortModels;
public class BucketSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Bucket;
    public override string TimeComplexity => "O(n+k)";
    public override string SpaceComplexity => "O(n+k)";
    public override string YoutubeLink => "https://youtu.be/VuXbEb5ywrU?si=G_JHV1tPPCvVnceg";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bucket-sort-2/";

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