using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViSort.Models.SortModels;
using ViSort.Utils;
using ViSort.Windows;

namespace ViSort.Pages;

/// <summary>
/// Interaction logic for VisualisePage.xaml
/// </summary>
public partial class VisualisePage : Page
{
    private int ElementCount = 100;
    private SortTypes SelectedSortType = default;
    private RandomGenTypes selectedGenType = default;

    public VisualisePage()
    {
        InitializeComponent();
        BubbleButton.IsChecked = true;
        NormalGenButton.IsChecked = true;
    }

    private void SortAlgorithm_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            SelectedSortType = radioButton.Content.ToString() switch
            {
                "Bubble Sort" => SortTypes.Bubble,
                "Bucket Sort" => SortTypes.Bucket,
                "Counting Sort" => SortTypes.Counting,
                "Selection Sort" => SortTypes.Selection,
                "Insertion Sort" => SortTypes.Insertion,
                "Merge Sort" => SortTypes.Merge,
                "Quick Sort" => SortTypes.Quick,
                "Heap Sort" => SortTypes.Heap,
                "Radix Sort" => SortTypes.Radix,
                "Shell Sort" => SortTypes.Shell,
                "Tim Sort" => SortTypes.Tim,
                "Tree Sort" => SortTypes.Tree,
                _ => throw new ArgumentException("Unsupported sorting algorithm selected")
            };
            UpdateSelectedSection();
        }
    }

    private void ArrayGenerationMethod_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            selectedGenType = radioButton.Content.ToString() switch
            {
                "Normal" => RandomGenTypes.Normal,
                "Sorted" => RandomGenTypes.Sorted,
                "Sorted Reverse" => RandomGenTypes.SortedReverse,
                "Nearly Sorted" => RandomGenTypes.NearlySorted,
                "Mirror" => RandomGenTypes.Mirror,
                _ => throw new ArgumentException("Unsupported random")
            };
            UpdateSelectedSection();
        }
    }

    private void UpdateSelectedSection()
    {
        SelectedSectionTextBlock.Text = $"Algorithm: {SelectedSortType}, Array: {selectedGenType}";
    }

    private void ElementCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (ElementCountNumberBox != null && !ElementCountNumberBox.IsFocused)
        {
            ElementCount = (int)e.NewValue;
            ElementCountNumberBox.Text = ElementCount.ToString();
        }
    }

    private void ElementCountNumberBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UpdateSliderFromNumberBox();
    }

    private void ElementCountNumberBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            UpdateSliderFromNumberBox();
        }
    }

    private void UpdateSliderFromNumberBox()
    {
        if (int.TryParse(ElementCountNumberBox.Text, out int value) && value >= ElementCountSlider.Minimum && value <= ElementCountSlider.Maximum)
        {
            ElementCount = value;
            ElementCountSlider.Value = value;
        }
        else
        {
            MessageBox.Show("Please enter a valid integer within the range.");
            ElementCountNumberBox.Text = ElementCount.ToString();
        }
    }

    private void ElementCountNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextNumeric(e.Text);
    }

    private bool IsTextNumeric(string text)
    {
        return int.TryParse(text, out _);
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        VisualiseWindow visualiseWindow = new(ElementCount, SelectedSortType, selectedGenType);
        visualiseWindow.Show();
    }
}