using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ViSort.Models;

public class DrawRectangle(Canvas? canvas = null, int threadDelay = 0)
{
    public int ThreadDelay { get; set; } = threadDelay;
    public Canvas DrawCanvas { get; } = canvas ?? new();

    public void ShowAllElementsBlue(List<int> elements, Color brushColor)
    {
        SetAllRectangleColor(elements, brushColor);
    }

    public async Task SwapElementsAsync(List<int> elements, int index1, int index2)
    {
        SetOneRectangleColor(index1, Colors.DarkGreen);
        SetOneRectangleColor(index2, Colors.DarkRed);
        await Task.Delay(ThreadDelay);
        Utils.Utils.Swap(elements, index1, index2);
        await Task.Delay(ThreadDelay / 2);
        DrawOne(elements, index1, Colors.DarkRed);
        DrawOne(elements, index2, Colors.DarkGreen);
        await Task.Delay(ThreadDelay / 2);
        DrawRectangleOnCanvas(elements, Colors.Gray);
    }

    private void DrawOne(List<int> elements, int index, Color brushColor)
    {
        int maxValueOfElement = elements.Max();
        DrawCanvas.Children.RemoveAt(index);
        double width = DrawCanvas.ActualWidth / elements.Count;
        double height = DrawCanvas.ActualHeight;
        Rectangle rect = new()
        {
            Width = width,
            Height = height * elements[index] / maxValueOfElement,
            Fill = new SolidColorBrush(brushColor),
            Stroke = new SolidColorBrush(Colors.White),
            StrokeThickness = 0.5
        };
        Canvas.SetLeft(rect, index * width);
        Canvas.SetBottom(rect, 0);
        DrawCanvas.Children.Add(rect);
    }

    public void DrawRectangleOnCanvas(List<int> elements, Color brushColor)
    {
        int maxValueOfElement = elements.Max();
        DrawCanvas.Children.Clear();
        double width = DrawCanvas.ActualWidth / elements.Count;
        double height = DrawCanvas.ActualHeight;
        for (int i = 0; i < elements.Count; i++)
        {
            Rectangle rect = new()
            {
                Width = width,
                Height = height * elements[i] / maxValueOfElement,
                Fill = new SolidColorBrush(brushColor),
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

    public void SetAllRectangleColor(List<int> elements, Color color)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            SetOneRectangleColor(i, color);
        }
    }
}