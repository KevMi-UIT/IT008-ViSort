﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ViSort.Models;
using ViSort.Types;
using ViSort.Utils;

namespace ViSort.Windows;

public partial class VisualiseWindow : Window
{
    private readonly SortTypes SelectedSortType;
    private readonly SortModel SelectedSort;
    public DrawRectangle drawRectangle;

    public VisualiseWindow(int _ElementCount, SortTypes _SelectedSortAlgorithm, GenRandomListTypes _SelectedArrayGenerationMethod)
    {
        InitializeComponent();
        drawRectangle = new(SortVisualisation, 100);
        SelectedSortType = _SelectedSortAlgorithm;
        SelectedSort = SortUtils.InstantiateSort(SelectedSortType, GenRandomList.GenList(_ElementCount, _SelectedArrayGenerationMethod), drawRectangle);
        AlgorithmName.Content = SelectedSort.SortType.ToString() + " Sort";
        TimeComplexity.Content = "Time complexity: " + SelectedSort.TimeComplexity;
        SpaceComplexity.Content = "Space complexity: " + SelectedSort.SpaceComplexity;
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
        _ = SelectedSort.BeginSortingAsync();
        PlayButton.IsEnabled = false;
    }

    private void SortVisualisation_Loaded(object sender, RoutedEventArgs e)
    {
        drawRectangle.DrawRectangleOnCanvas(SelectedSort.Elements, Colors.Gray);
    }

    private void SortVisualisation_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        SortVisualisation.Children.Clear();
        drawRectangle.DrawRectangleOnCanvas(SelectedSort.Elements, Colors.Gray);
    }
}