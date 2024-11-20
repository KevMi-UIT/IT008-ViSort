using MongoDB.Driver;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
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
using ViSort.Draw;
using ViSort.Models;
using ViSort.Models.SortModels;
using ViSort.Utils;
using Windows.Security.Cryptography.Certificates;

namespace ViSort;

public partial class VisualiseWindow : Window
{
    private readonly SortTypes SelectedSortType;
    private readonly SortModel SelectedSort;
    public DrawRectangle drawRectangle;
    public VisualiseWindow(int _ElementCount, SortTypes _SelectedSortAlgorithm, RandomGenTypes _SelectedArrayGenerationMethod)
    {
        InitializeComponent();
        SortVisualisation.Height = GenRandomList.MAX;
        drawRectangle = new(SortVisualisation);
        // TODO: change thread delay => DONE
        drawRectangle.ThreadDelay = 10; //default value
        SelectedSortType = _SelectedSortAlgorithm;
        SelectedSort = SelectedSortType switch
        {
            SortTypes.Bubble => new BubbleSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Bucket => throw new NotImplementedException(),
            SortTypes.Counting => throw new NotImplementedException(),
            SortTypes.Selection => new SelectionSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Insertion => new InsertionSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Merge => throw new NotImplementedException(),
            SortTypes.Quick => throw new NotImplementedException(),
            SortTypes.Heap => new HeapSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Radix => new RadixSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle), //Chua thay doi phan Swap => Chua visualise duoc
            SortTypes.Shell => new ShellSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Tim => throw new NotImplementedException(),
            SortTypes.Tree => throw new NotImplementedException(),
            _ => throw new NotImplementedException("Sort hasn't been implemented")
        };

        SelectedSort.DrawRect.DrawRectangleOnCanvas(SelectedSort.Elements);
    }

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (SpeedNumberBox != null && !SpeedNumberBox.IsFocused)
        {
            drawRectangle.ThreadDelay = (int)e.NewValue;
            SpeedNumberBox.Text = drawRectangle.ThreadDelay.ToString();
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
            drawRectangle.ThreadDelay = value;
            SpeedSlider.Value = value;
        }
        else
        {
            MessageBox.Show("Please enter a valid integer within the range.");
            SpeedNumberBox.Text = drawRectangle.ThreadDelay.ToString();
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
        SelectedSort.BeginSorting();
        IsEnabled = false;
    }
}