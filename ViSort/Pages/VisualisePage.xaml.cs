using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using ViSort.Types;
using ViSort.Utils;
using ViSort.Windows;
using Wpf.Ui.Controls;
using static ViSort.Exceptions.GenRandomListExceptions;
using static ViSort.Exceptions.SortExceptions;

namespace ViSort.Pages;

public partial class VisualisePage : Page
{
    public VisualisePage()
    {
        InitializeComponent();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        int elementCount = (int)ElementCountSlider.Value;
        SortTypes selectedSortType = SortUtils.GetSortType(((ComboBoxItem)SortSelectionComboBox.SelectedItem).Content.ToString()!);
        GenRandomListTypes selectedGenType = ((ComboBoxItem)RandomGenComboBox.SelectedItem).Content.ToString() switch
        {
            "Normal" => GenRandomListTypes.Normal,
            "Sorted" => GenRandomListTypes.Sorted,
            "Sorted Reverse" => GenRandomListTypes.SortedReverse,
            "Nearly Sorted" => GenRandomListTypes.NearlySorted,
            "Mirror" => GenRandomListTypes.Mirror,
            "Adjacent" => GenRandomListTypes.Adjacent,
            "Nearly Adjacent" => GenRandomListTypes.NearlyAdjacent,
            _ => throw new GenRandomListNotImplemented()
        };
        VisualiseWindow visualiseWindow = new(elementCount, selectedSortType, selectedGenType);
        visualiseWindow.Show();
    }
}