using System.Windows.Controls;
using ViSort.Draw;
using ViSort.Models;
using ViSort.Types;
using ViSort.Utils;
using ViSort.ViewModels;

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
        SortTypes sortType = SortUtils.GetSortType(args.QueryText);
        SortModel sortModel = SortUtils.InstantiateSort(sortType, [], new DrawRectangle(new Canvas()));
        if (DataContext is SortsInfoViewModel sortsInfoViewModel)
        {
            sortsInfoViewModel.Model = new(sortModel);
        }
    }

    private void AutoSuggestBox_SuggestionChosen(WpfUiControls.AutoSuggestBox sender, WpfUiControls.AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is string selectedItem)
        {
            SortTypes sortType = SortUtils.GetSortType(selectedItem);
            SortModel sortModel = SortUtils.InstantiateSort(sortType, [], new DrawRectangle(new Canvas()));
            if (DataContext is SortsInfoViewModel sortsInfoViewModel)
            {
                sortsInfoViewModel.Model = new(sortModel);
            }
        }
    }
}