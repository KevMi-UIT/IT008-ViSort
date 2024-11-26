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
using ViSort.Types;
using ViSort.Utils;
using Windows.Security.Cryptography.Certificates;
using static ViSort.Exceptions.SortExceptions;

namespace ViSort.Windows;

public partial class VisualiseWindow : Window
{
    private readonly SortTypes SelectedSortType;
    private readonly SortModel SelectedSort;
    public DrawRectangle drawRectangle;

    public VisualiseWindow(int _ElementCount, SortTypes _SelectedSortAlgorithm, GenRandomListTypes _SelectedArrayGenerationMethod)
    {
        InitializeComponent();
        SortVisualisation.Height = GenRandomList.MAX;
        drawRectangle = new(SortVisualisation, 100);
        SelectedSortType = _SelectedSortAlgorithm;
        SelectedSort = SelectedSortType switch
        {
            SortTypes.Bubble => new BubbleSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Bucket => new BucketSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Counting => new CountingSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Selection => new SelectionSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Insertion => new InsertionSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Merge => new MergeSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Quick => new QuickSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Heap => new HeapSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Radix => new RadixSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Shell => new ShellSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Tim => new TimSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            SortTypes.Tree => new TreeSort(GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle),
            _ => throw new SortNotImplemented()
        };

    }

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (SpeedNumberBox != null && !SpeedNumberBox.IsFocused)
        {
            int temp = Math.Abs((int)e.NewValue - 100);
            drawRectangle.ThreadDelay = (temp != 0) ? temp : 1;
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
            int temp = Math.Abs(value - 100);
            drawRectangle.ThreadDelay = (temp != 0) ? temp : 1;
            SpeedSlider.Value = 101 - temp;
            SpeedNumberBox.Text = drawRectangle.ThreadDelay.ToString();
        }
        else
        {
            MessageBox.Show("Please enter a valid integer between 1 and 100 ms.");
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
        Task task = SelectedSort.BeginSorting();
        PlayButton.IsEnabled = false;
    }

    private void Window_Activated(object sender, EventArgs e)
    {
        SortVisualisation.Width = this.ActualWidth - 20;
        SelectedSort.DrawRect.DrawRectangleOnCanvas(SelectedSort.Elements, Colors.Black);
    }
}