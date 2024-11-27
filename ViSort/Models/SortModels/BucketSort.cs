using System.Windows.Media;
using ViSort.Types;

namespace ViSort.Models.SortModels;
public class BucketSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle)
{
    public override SortTypes SortType => SortTypes.Bucket;
    public override string TimeComplexity => "O(n+k)";
    public override string SpaceComplexity => "O(n+k)";
    public override string YoutubeLink => "https://youtu.be/VuXbEb5ywrU?si=G_JHV1tPPCvVnceg";
    public override string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bucket-sort-2/";

    public override async Task BeginAlgorithmAsync()
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
        int MaxValue = Elements.Max();
        List<int>[] buckets = new List<int>[Elements.Count];
        for (int i = 0; i < buckets.Length; i++)
        {
            Step++;
            buckets[i] = [];
        }
        for (int i = 0; i < Elements.Count; i++)
        {
            Step++;
            int bucketIndex = Elements[i] * buckets.Length / (MaxValue + 1);
            buckets[bucketIndex].Add(Elements[i]);
            DrawRect.DrawRectangleOnCanvas(Elements, Colors.Gray);
            await Task.Delay(10);
        }
        for (int i = 0; i < buckets.Length; i++)
        {
            Step++;
            if (buckets[i].Count > 0)
            {
                DrawRect.DrawRectangleOnCanvas(buckets[i], Colors.Red);
                await Task.Delay(DrawRect.ThreadDelay * 10);
                await InsertionSort(buckets[i]);
                DrawRect.DrawRectangleOnCanvas(buckets[i], Colors.Green);
                await Task.Delay(DrawRect.ThreadDelay * 10);
            }
        }
        int index = 0;
        for (int i = 0; i < buckets.Length; i++)
        {
            foreach (var element in buckets[i])
            {
                Step++;
                Elements[index++] = element;
                DrawRect.DrawRectangleOnCanvas(Elements, Colors.Orange);
                await Task.Delay(DrawRect.ThreadDelay);
            }
        }
        DrawRect.DrawRectangleOnCanvas(Elements, Colors.Gray);
    }
}