using MongoDB.Driver;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using ViSort.Sorts;
using ViSort.Utils;
using Windows.Security.Cryptography.Certificates;

namespace ViSort;

/// <summary>
/// Interaction logic for DisplayWindow.xaml
/// </summary>
public partial class DisplayWindow : Window
{
    private int ThreadDelay;
    private readonly SortTypes SelectedSortType;
    private readonly BaseSort SelectedSort;

    public DisplayWindow(int _ElementCount, SortTypes _SelectedSortAlgorithm, RandomGenTypes _SelectedArrayGenerationMethod)
    {
        ThreadDelay = 100;
        SelectedSortType = _SelectedSortAlgorithm;
        SelectedSort = SelectedSortType switch
        {
            SortTypes.Bubble => new BubbleSort(),
            SortTypes.Bucket => throw new NotImplementedException(),
            SortTypes.Counting => throw new NotImplementedException(),
            SortTypes.Selection => throw new NotImplementedException(),
            SortTypes.Insertion => throw new NotImplementedException(),
            SortTypes.Merge => throw new NotImplementedException(),
            SortTypes.Quick => throw new NotImplementedException(),
            SortTypes.Heap => throw new NotImplementedException(),
            SortTypes.Radix => throw new NotImplementedException(),
            SortTypes.Shell => throw new NotImplementedException(),
            SortTypes.Tim => throw new NotImplementedException(),
            SortTypes.Tree => throw new NotImplementedException(),
            _ => throw new NotImplementedException("Sort hasn't been implemented")
        };
        InitializeComponent();
        SortVisualisation.Height = GenRandomList.MAX;
        SelectedSort.MaxWidth = (int)(SortVisualisation.Width / _ElementCount);
        SelectedSort.MaxHeight = (int)SortVisualisation.Height;
        SelectedSort.DrawingCanvas = SortVisualisation;
        SelectedSort.Elements = GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod);
        SelectedSort.DrawRectangleOnCanvas(SelectedSort.Elements);
    }

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (SpeedNumberBox != null && !SpeedNumberBox.IsFocused)
        {
            ThreadDelay = (int)e.NewValue;
            SpeedNumberBox.Text = ThreadDelay.ToString();
        }
    }

    private void SpeedNumberBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UpdateSliderFromNumberBox();
    }

    private void SpeedNumberBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            UpdateSliderFromNumberBox();
        }
    }

    private void UpdateSliderFromNumberBox()
    {
        if (int.TryParse(SpeedNumberBox.Text, out int value) && value >= SpeedSlider.Minimum && value <= SpeedSlider.Maximum)
        {
            ThreadDelay = value;
            SpeedSlider.Value = value;
        }
        else
        {
            MessageBox.Show("Please enter a valid integer within the range.");
            SpeedNumberBox.Text = ThreadDelay.ToString();
        }
    }

    private void SpeedNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextNumeric(e.Text);
    }

    private static bool IsTextNumeric(string text)
    {
        return int.TryParse(text, out _);
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSort.ElementCount = SelectedSort.Elements.Count;
        SelectedSort.ThreadDelay = ThreadDelay;
        SelectedSort.BeginSorting();
    }
}