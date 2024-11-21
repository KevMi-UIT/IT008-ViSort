﻿using System;
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