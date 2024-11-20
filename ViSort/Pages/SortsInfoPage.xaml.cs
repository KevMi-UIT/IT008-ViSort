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
using ViSort.ViewModels;

namespace ViSort.Pages;

public partial class SortsInfoPage : Page
{
    public SortsInfoViewModel? SortInfoVM { get; private set; }

    public SortsInfoPage()
    {
        InitializeComponent();
        AutoSuggestBox.OriginalItemsSource = new List<string>
        {
            "Bubble Sort",
            "Bucket Sort",
            "Counting Sort",
            "Heap Sort",
            "Insertion Sort",
            "Merge Sort",
            "Quick Sort",
            "Radix Sort",
            "Selection Sort",
            "Shell Sort",
            "Tim Sort",
            "Tree Sort"
        };
        AutoSuggestBox.Focus();
    }

    private void AutoSuggestBox_QuerySubmitted(WpfUiControls.AutoSuggestBox sender, WpfUiControls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        //SortTypes sortType = SortUtils.GetSortType(args.QueryText);
        //SortInfoVM = SortUtils.InstantiateSort();
    }
}