using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Visort.Drawing;
using ViSort.Utils;
namespace ViSort.Sorts;

public abstract class BaseSort
{
    public string? TimeComplexity { get; set; }
    public string? SpaceComplexity { get; set; }
    public required Canvas DrawingCanvas { get; set; }
    public int MaxWidth;
    public int MaxHeight;
    public int ThreadDelay;
    public List<int> Elements = [];
    public abstract int ElementCount { get; set; }
    public abstract void BeginAlgorithm();

    public void BeginSorting()
    {
        BeginAlgorithm();
        ShowCompletedDisplay();
    }

    private void ShowCompletedDisplay()
    {
        ShowAllElementsBlue();
    }

    public void ShowAllElementsBlue()
    {
        int adjustedDelay = ThreadDelay == 200 ? 80 : ThreadDelay == 0 ? 1 : ThreadDelay;

        for (int i = 0; i < Elements.Count; i++)
        {
            DrawRectangleOnCanvas(Elements);

        }
    }

    public async Task SwapElementsAsync(int index1, int index2)
    {
        SetRectangleColor(index1, Colors.Red);
        SetRectangleColor(index2, Colors.Red);
        await Task.Delay(10);
        int temp = Elements[index1];
        Elements[index1] = Elements[index2];
        Elements[index2] = temp;
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            DrawRectangleOnCanvas(Elements);
        });
        await Task.Delay(10);
        SetRectangleColor(index1, Colors.Black);
        SetRectangleColor(index2, Colors.Black);
        await Task.Delay(10);
    }

    public void DrawRectangleOnCanvas(List<int> Elements)
    {
        DrawingCanvas.Children.Clear();
        double width = DrawingCanvas.Width / Elements.Count;
        double height = DrawingCanvas.Height;
        for (int i = 0; i < Elements.Count; i++)
        {
            Rectangle rect = new Rectangle
            {
                Width = width,
                Height = (height * Elements[i]) / ViSort.Utils.GenRandomList.MAX,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.5
            };
            Canvas.SetLeft(rect, i * width);
            Canvas.SetBottom(rect, 0);
            DrawingCanvas.Children.Add(rect);
        }
    }

    private void SetRectangleColor(int index, Color color)
    {
        if (index < 0 || index >= DrawingCanvas.Children.Count)
            return;
        else if (DrawingCanvas.Children[index] is Rectangle rect)
        {
            rect.Fill = new SolidColorBrush(color);
        }
    }

    public void SetComplexity(int complexityRangeValue)
    {
        switch (complexityRangeValue)
        {
            case 0:
                TimeComplexity = "O(nLog(n))";
                SpaceComplexity = "O(1)";
                break;
            case 1:
                TimeComplexity = "O(nLog(n))";
                SpaceComplexity = "O(Log(n))";
                break;
            case 2:
                TimeComplexity = "O(nLog(n))";
                SpaceComplexity = "O(n)";
                break;
            case 3:
                TimeComplexity = "O(nk)";
                SpaceComplexity = "O(n+k)";
                break;
            case 4:
                TimeComplexity = "O(n²)";
                SpaceComplexity = "O(1)";
                break;
        }
    }
}