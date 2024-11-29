using System.Windows;
using System.Windows.Media;
using ViSort.Models;
using ViSort.Types;
using ViSort.Utils;

namespace ViSort.Windows;

enum ButtonState
{
    Play,
    End,
    Restart
}

public partial class VisualiseWindow : Window
{
    private readonly SortModel SelectedSort;
    private readonly List<int> OriginalElements;
    private ButtonState CurrentState = ButtonState.Play;

    public VisualiseWindow(int elementCount, SortTypes sortType, GenRandomListTypes genRandType)
    {
        InitializeComponent();
        DrawRectangle drawRectangle = new(SortVisualisation, 250);
        OriginalElements = GenRandomList.GenList(elementCount, genRandType);
        SelectedSort = SortUtils.InstantiateSort(sortType, new(OriginalElements), drawRectangle);

        AlgorithmName.Content = SelectedSort.SortType.ToString() + " Sort";
        TimeComplexity.Content = "Time complexity: " + SelectedSort.TimeComplexity;
        SpaceComplexity.Content = "Space complexity: " + SelectedSort.SpaceComplexity;
    }

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (SelectedSort != null)
        {
            SelectedSort.DrawRect.ThreadDelay = (int)e.NewValue;
        }
    }

    private void PlayEndRestartButton_Click(object sender, RoutedEventArgs e)
    {
        switch (CurrentState)
        {
            case ButtonState.Play:
                PlayEndRestartButton.Content = "End";
                CurrentState = ButtonState.End;
                _ = SelectedSort.BeginSortingAsync(QuickEndAction);
                break;
            case ButtonState.End:
                SelectedSort.DrawRect.ThreadDelay = 0;
                PlayEndRestartButton.Content = "Restart";
                CurrentState = ButtonState.Restart;
                break;
            case ButtonState.Restart:
                SpeedSlider.Value = 250;
                SelectedSort.DrawRect.ThreadDelay = 250;
                SelectedSort.Elements = new(OriginalElements);
                PlayEndRestartButton.Content = "End";
                CurrentState = ButtonState.End;
                DrawOnCanvas();
                _ = SelectedSort.BeginSortingAsync(QuickEndAction);
                break;
            default:
                // Handled
                break;
        }
    }

    private void SortVisualisation_Loaded(object sender, RoutedEventArgs e)
    {
        DrawOnCanvas();
    }

    private void SortVisualisation_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        SortVisualisation.Children.Clear();
        DrawOnCanvas();
    }

    private void QuickEndAction()
    {
        PlayEndRestartButton.Content = "Restart";
        CurrentState = ButtonState.Restart;
    }

    private void DrawOnCanvas()
    {
        SelectedSort.DrawRect.DrawRectangleOnCanvas(SelectedSort.Elements, Colors.Gray);
    }
}