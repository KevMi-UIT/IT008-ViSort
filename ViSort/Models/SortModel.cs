using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using ViSort.Draw;
using ViSort.Models.SortModels;
using ViSort.Types;
using ViSort.Utils;

namespace ViSort.Models;

public abstract class SortModel(List<int> _element, DrawRectangle _drawRectangle)
{
    public abstract SortTypes SortType { get; }
    public abstract string TimeComplexity { get; }
    public abstract string SpaceComplexity { get; }
    public abstract string YoutubeLink { get; }
    public abstract string GeeksForGeeksLink { get; }
    public int Step { get; protected set; } = 0;
    public List<int> Elements { get; set; } = _element;

    public readonly DrawRectangle DrawRect = _drawRectangle;
    public abstract Task BeginAlgorithm();

    public async Task BeginSortingAsync()
    {
        await BeginAlgorithm();
        DrawRect.ShowAllElementsBlue(Elements, Colors.Blue);
    }
}