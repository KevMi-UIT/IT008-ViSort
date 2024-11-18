using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Visort.Drawing;

public class DrawRectangle
{
    public required Canvas DrawingCanvas;

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
                Height = (height * Elements[i]) / 999,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.5
            };
            Canvas.SetLeft(rect, i * width);
            Canvas.SetBottom(rect, 0);
            DrawingCanvas.Children.Add(rect);
        }
    }

    public async Task SwapElementsAsync(int index1, int index2, List<int> Elements)
    {
        if (index1 >= 0 && index1 < DrawingCanvas.Children.Count &&
            index2 >= 0 && index2 < DrawingCanvas.Children.Count)
        {
            await DrawingCanvas.Dispatcher.InvokeAsync(() =>
            {
                SetRectangleColor(index1, Colors.DarkRed);
                SetRectangleColor(index2, Colors.DarkRed);
            });
            await Task.Delay(200);
            int tempValue = Elements[index1];
            Elements[index1] = Elements[index2];
            Elements[index2] = tempValue;
            await DrawingCanvas.Dispatcher.InvokeAsync(() =>
            {
                SetRectangleColor(index1, Colors.Black);
                SetRectangleColor(index2, Colors.Black);
            });
        }
    }

    private void SetRectangleColor(int index, Color color)
    {
        if (index < 0 || index >= DrawingCanvas.Children.Count)
        {
            return;
        }
        if (DrawingCanvas.Children[index] is Rectangle rect)
        {
            rect.Fill = new SolidColorBrush(color);
        }
    }
}