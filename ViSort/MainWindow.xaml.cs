﻿namespace ViSort;
using System;
using System.Collections.Generic;
using System.IO;
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
using ViSort.QuizWindow;
using Windows.System;
using Wpf.Ui.Controls;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OpenQuizWindow(object sender, RoutedEventArgs e)
    {
        if (App.User == null && await App.EstablishDBConnectionAsync())
        {
            var UserFillForm = new UserFillForm();
            UserFillForm.ShowDialog();
        }

        this.Visibility = Visibility.Hidden;
        var QuizWindow = new QuizWindow.QuizWindow();
        QuizWindow.Closed += (s, args) => this.Visibility = Visibility.Visible;
        QuizWindow.Show();
    }

    private async void ScoreBoard_Click(object sender, RoutedEventArgs e)
    {
        if (!await App.EstablishDBConnectionAsync())
        {
            return;
        }
        var ScoreBoard = new ScoreBoard.ScoreBoard();
        ScoreBoard.ShowDialog();

    }

    private void UserProfileButton_Click(object sender, RoutedEventArgs e)
    {
        var UserProfile = new UserProfile.UserProfile();
        UserProfile.ShowDialog();

        if (App.User != null)
        {
            Status_Icon.Symbol = SymbolRegular.PersonAvailable24;
        }
    }
}