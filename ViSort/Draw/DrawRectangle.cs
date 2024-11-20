using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ViSort.Draw;

public class DrawRectangle(Canvas _canvas, int _threadDelay = 0)
{
    public int ThreadDelay { get; set; } = _threadDelay;
    public Canvas DrawCanvas { get; } = _canvas;
    public void ShowAllElementsBlue(List<int> Elements)
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            DrawRectangleOnCanvas(Elements);
        }
    }

    public async Task SwapElementsAsync(List<int> Elements, int index1, int index2)
    {
        SetRectangleColor(index1, Colors.Red);
        SetRectangleColor(index2, Colors.Red);
        await Task.Delay(ThreadDelay * 2);
        Utils.Utils.Swap(Elements, index1, index2);
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            DrawRectangleOnCanvas(Elements);
        });
        await Task.Delay(ThreadDelay);
        SetRectangleColor(index1, Colors.Black);
        SetRectangleColor(index2, Colors.Black);
        await Task.Delay(ThreadDelay * 2);
    }

    public void DrawRectangleOnCanvas(List<int> Elements)
    {
        DrawCanvas.Children.Clear();
        double width = DrawCanvas.Width / Elements.Count;
        double height = DrawCanvas.Height;
        for (int i = 0; i < Elements.Count; i++)
        {
            Rectangle rect = new()
            {
                Width = width,
                Height = (height * Elements[i]) / ViSort.Utils.GenRandomList.MAX,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.5
            };
            Canvas.SetLeft(rect, i * width);
            Canvas.SetBottom(rect, 0);
            DrawCanvas.Children.Add(rect);
        }
    }

    private void SetRectangleColor(int index, Color color)
    {
        if (index < 0 || index >= DrawCanvas.Children.Count)
            return;
        else if (DrawCanvas.Children[index] is Rectangle rect)
        {
            rect.Fill = new SolidColorBrush(color);
        }
    }
}