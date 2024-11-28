using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Wpf.Ui.Appearance;
namespace ViSort.Models;

public class DrawRectangle(Canvas _canvas, int _threadDelay = 0)
{
    public int MaxValueOfElement { get; set; } = Utils.GenRandomList.MAX;
    public int ThreadDelay { get; set; } = _threadDelay;
    public Canvas DrawCanvas { get; } = _canvas;

    public void ShowAllElementsBlue(List<int> Elements, Color BrushColor)
    {
        SetAllRectangleColor(Elements, BrushColor);
    }

    public async Task SwapElementsAsync(List<int> Elements, int index1, int index2)
    {
        SetOneRectangleColor(index1, Colors.Red);
        SetOneRectangleColor(index2, Colors.Red);
        await Task.Delay(ThreadDelay);
        Utils.Utils.Swap(Elements, index1, index2);
        await Task.Delay(ThreadDelay);
        DrawRectangleOnCanvas(Elements, Colors.Gray);
        SetOneRectangleColor(index1, Colors.Gray);
        SetOneRectangleColor(index2, Colors.Gray);
    }

    public void DrawRectangleOnCanvas(List<int> Elements, Color BrushColor)
    {
        DrawCanvas.Children.Clear();
        double width = DrawCanvas.ActualWidth / Elements.Count;
        double height = DrawCanvas.ActualHeight;
        for (int i = 0; i < Elements.Count; i++)
        {
            Rectangle rect = new()
            {
                Width = width,
                Height = height * Elements[i] / MaxValueOfElement,
                Fill = new SolidColorBrush(BrushColor),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.5
            };
            Canvas.SetLeft(rect, i * width);
            Canvas.SetBottom(rect, 0);
            DrawCanvas.Children.Add(rect);
        }
    }

    public void SetOneRectangleColor(int index, Color color)
    {
        if (index < 0 || index >= DrawCanvas.Children.Count)
            return;
        if (DrawCanvas.Children[index] is Rectangle rect)
        {
            rect.Fill = new SolidColorBrush(color);
        }
    }

    public void SetAllRectangleColor(List<int> Elements, Color color)
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            SetOneRectangleColor(i, color);
        }
    }
}