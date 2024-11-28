using System.Windows.Controls;
using ViSort.Models;
using ViSort.Types;
using ViSort.Utils;
using ViSort.ViewModels;
using static ViSort.Exceptions.SortExceptions;

namespace ViSort.Views;
/// <summary>
/// Interaction logic for SortsInfoView.xaml
/// </summary>
public partial class SortsInfoView : UserControl
{
    public SortsInfoView()
    {
        InitializeComponent();
    }

    private void AutoSuggestBox_QuerySubmitted(WpfUiControls.AutoSuggestBox sender, WpfUiControls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        DisplayChosenSort(args.QueryText);
    }

    private void AutoSuggestBox_SuggestionChosen(WpfUiControls.AutoSuggestBox sender, WpfUiControls.AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is string selectedItem)
        {
            DisplayChosenSort(selectedItem);
        }
    }

    private void DisplayChosenSort(string selectedItem)
    {
        try
        {
            SortTypes sortType = SortUtils.GetSortType(selectedItem);
            SortModel sortModel = SortUtils.InstantiateSort(sortType);
            if (DataContext is SortsInfoViewModel sortsInfoViewModel)
            {
                sortsInfoViewModel.Model = new(sortModel);
            }
        }
        catch (SortUdefined)
        {
            // skip
        }
    }
}