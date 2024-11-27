﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViSort.Models;
using ViSort.Types;
using ViSort.Utils;
using ViSort.Windows;

namespace ViSort.Pages;

/// <summary>
/// Interaction logic for QuizPage.xaml
/// </summary>
public partial class QuizPage : Page
{
    private readonly List<QuizModel> QuestionList = [];
    private readonly List<RadioButton> _radioButtons = new();
    private int currentQuestionIndex = 0;
    public QuizPage()
    {
        List<List<int>> questions = App.QUIZ_LIST.Take(5).Randomize().ToList();
        for (int i = 0; i < questions.Count; ++i)
        {
            QuizModel q = new(i + 1, string.Join(", ", questions[i]));
            QuestionList.Add(q);
        }
        InitializeComponent();
        DisplayCurrentQuestion();
    }

    private void DisplayCurrentQuestion()
    {
        PrevQuestionButton.IsEnabled = currentQuestionIndex > 0;
        NextQuestionButton.IsEnabled = currentQuestionIndex < QuestionList.Count - 1;

        QuestionTextBlock.Text = QuestionList[currentQuestionIndex].Content;

        var selectedAnswer = QuestionList[currentQuestionIndex].SelectedAnswer;
        if (selectedAnswer != null)
        {
            SetSelectedRadioButton(selectedAnswer);
        }
        else
        {
            ResetRadioButtons();
        }
    }

    private void PrevQuestionButton_Click(object sender, RoutedEventArgs e)
    {
        currentQuestionIndex--;
        DisplayCurrentQuestion();
    }

    private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
    {
        currentQuestionIndex++;
        DisplayCurrentQuestion();
    }
    private int CountAnswer()
    {
        int count = 0;
        foreach (var q in QuestionList)
        {
            if (q.SelectedAnswer != null)
            {
                count++;
            }
        }
        return count;
    }

    private void Answer_Checked(object sender, RoutedEventArgs e)
    {
        // Xác định nút radio nào đã được chọn
        if (sender is RadioButton selectedRadioButton)
        {
            SortTypes sortTypes = selectedRadioButton.Content switch
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
                _ => default
            };
            QuestionList[currentQuestionIndex].SelectAnswer(sortTypes);
        }
    }

    private void ResetRadioButtons()
    {
        foreach (var radioButton in MultipleChoiceWrapPanel.Children.OfType<RadioButton>())
        {
            radioButton.IsChecked = false;
        }
    }

    private void SetSelectedRadioButton(SortTypes? selectedAnswer)
    {
        string selectedAnswerString = $"{selectedAnswer.ToString() as string} Sort";
        foreach (var radioButton in MultipleChoiceWrapPanel.Children.OfType<RadioButton>())
        {
            if (radioButton.Content.ToString() == selectedAnswerString)
            {
                radioButton.IsChecked = true;
                break;
            }
        }
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        //TODO: change value input to CalcScore
        int score = Utils.Utils.CalcScore(0, QuestionList.Count, CountAnswer());

        if (App.User != null && App.UserSvc != null)
        {
            App.User.SetScore(score);
            await App.UserSvc.UpdateScoreAsync(App.User);

            WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
            {
                Title = "Info",
                Content = "Quiz ended.\nChoose 'OK' button to navigate to the ScoreBoard and 'Close' button to navigate to the Homepage.",
                PrimaryButtonText = "OK",
            }.ShowDialogAsync();
            if (result == WpfUiControls.MessageBoxResult.Primary)
            {
                MainWindow.RootNavigationView.Navigate(typeof(ScoreBoardPage));
            }
            else
            {
                MainWindow.RootNavigationView.Navigate(typeof(HomePage));
            }
        }
        else
        {
            WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
            {
                Title = "Info",
                Content = "Quiz ended.\nChoose 'OK' button to navigate to the Homepage and 'Close' button to answer quiz again.",
                PrimaryButtonText = "OK",
            }.ShowDialogAsync();
            if (result == WpfUiControls.MessageBoxResult.Primary)
            {
                MainWindow.RootNavigationView.Navigate(typeof(HomePage));
            }
            else
            {
                MainWindow.RootNavigationView.Navigate(typeof(QuizPage));
            }
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        foreach (var radioButton in MultipleChoiceWrapPanel.Children.OfType<RadioButton>())
        {
            _radioButtons.Add(radioButton);
        }
        Keyboard.Focus(MultipleChoiceWrapPanel);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key >= Key.D1 && e.Key <= Key.D9) // Phím 1-9
        {
            int index = e.Key - Key.D1; // Tính chỉ số dựa trên phím nhấn
            if (index < _radioButtons.Count)
            {
                _radioButtons[index].IsChecked = true;
            }
        }
        switch (e.Key)
        {
            case Key.D0:
                _radioButtons[9].IsChecked = true;
                break;
            case Key.Delete:
                _radioButtons[10].IsChecked = true;
                break;
            case Key.Escape:
                _radioButtons[11].IsChecked = true;
                break;
            case Key.Left:
                if (PrevQuestionButton.IsEnabled == true)
                {
                    PrevQuestionButton_Click(sender, e);
                }
                break;
            case Key.Right:
                if (NextQuestionButton.IsEnabled == true)
                {
                    NextQuestionButton_Click(sender, e);
                }
                break;
            case Key.Enter:
                SubmitButton_Click(sender, e);
                break;
            default:
                // All cases are handled
                break;
        }
    }
    private void InstructionButton_Click(object sender, RoutedEventArgs e)
    {
        InstructionFlyout.IsOpen = true;
        Keyboard.ClearFocus();
    }
    private void CloseInstructionFlyout_Click(object sender, RoutedEventArgs e)
    {
        InstructionFlyout.IsOpen = false;
        Keyboard.Focus(MultipleChoiceWrapPanel);
    }
}