using System.Windows.Media;
using ViSort.Types;
using Wpf.Ui.Appearance;

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
    public abstract Task BeginAlgorithmAsync();

    public async Task BeginSortingAsync()
    {
        await BeginAlgorithmAsync();
        DrawRect.ShowAllElementsBlue(Elements, ApplicationAccentColorManager.PrimaryAccent);
    }
}