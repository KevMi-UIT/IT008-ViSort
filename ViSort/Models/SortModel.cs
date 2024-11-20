using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using ViSort.Draw;
using ViSort.Models.Sorts;
using ViSort.Utils;
namespace ViSort.Models;

public abstract class SortModel(List<int> _element, DrawRectangle _drawRectangle)
{
    public abstract SortTypes SortType { get; }
    public abstract string TimeComplexity { get; }
    public abstract string SpaceComplexity { get; }
    public int Step { get; protected set; } = 0;
    public List<int> Elements { get; set; } = _element;
    public readonly DrawRectangle DrawRect = _drawRectangle;
    public abstract void BeginAlgorithm();

    public void BeginSorting()
    {
        BeginAlgorithm();
        DrawRect.ShowAllElementsBlue(Elements);
    }
    /*
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
    */
}