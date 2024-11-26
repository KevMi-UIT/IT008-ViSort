using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Windows.UI.WindowManagement;

namespace ViSort.Draw;

public class DrawRectangle(Canvas _canvas, int _threadDelay = 0)
{
    public int MaxValueOfElement { get; set; } = ViSort.Utils.GenRandomList.MAX;
    public int ThreadDelay { get; set; } = _threadDelay;
    public Canvas DrawCanvas { get; } = _canvas;
    private Color BrushColor { get; set; }

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
        //await Application.Current.Dispatcher.InvokeAsync(async () =>
        //{DrawRectangleOnCanvas(Elements, Colors.Black);});
        await Task.Delay(ThreadDelay);
        DrawRectangleOnCanvas(Elements, Colors.Black);
        SetOneRectangleColor(index1, Colors.Black);
        SetOneRectangleColor(index2, Colors.Black);
    }

    public void DrawRectangleOnCanvas(List<int> Elements, Color BrushColor)
    {
        DrawCanvas.Children.Clear();
        double width = DrawCanvas.Width / Elements.Count;
        double height = DrawCanvas.Height;
        for (int i = 0; i < Elements.Count; i++)
        {
            Rectangle rect = new()
            {
                Width = width,
                Height = (height * Elements[i]) / MaxValueOfElement,
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