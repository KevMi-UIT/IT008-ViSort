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
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenQuizWindow(object sender, RoutedEventArgs e)
    {

        var QuestFilePath = "QuizForm/question.txt";
        if (!File.Exists(QuestFilePath))
        {
            MessageBox.Show("Không tìm thấy tệp câu hỏi. \nVui lòng kiểm tra lại đường dẫn", "Lỗi nè", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (App.User == null)
        {
            var UserFillForm = new UserFillForm();
            UserFillForm.ShowDialog();
        }

        var QuizWindow = new QuizForm.QuizWindow();
        QuizWindow.ShowDialog();
    }

    private void CheckUserLogin_Click(object sender, RoutedEventArgs e)
    {
        if (App.User == null)
        {
            var loginWindow = new UserFillForm();
            loginWindow.ShowDialog();

            if (App.User != null)
            {
                Login_Icon.Symbol = SymbolRegular.AccessibilityCheckmark28;
            }
        }

    }

    private void ScoreBoard_Click(object sender, RoutedEventArgs e)
    {
        var ScoreBoard = new ScoreBoard.ScoreBoard();
        ScoreBoard.ShowDialog();
    }
}