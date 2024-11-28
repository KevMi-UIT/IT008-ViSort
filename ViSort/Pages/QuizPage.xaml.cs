using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
    private readonly List<RadioButton> RadioButtons = [];
    private readonly DrawRectangle DrawRect;
    private int currentQuestionIndex = 0;

    public QuizPage()
    {
        List<List<int>> questions = App.QUIZ_LIST.Take(5).Randomize().ToList();
        for (int i = 0; i < questions.Count; ++i)
        {
            QuizModel q = new(i + 1, string.Join(", ", questions[i]), questions[i]);
            QuestionList.Add(q);
        }
        InitializeComponent();
        DrawRect = new(QuestionCanvas);
        DisplayCurrentQuestion();
    }

    private void DisplayCurrentQuestion()
    {
        PrevQuestionButton.IsEnabled = currentQuestionIndex > 0;
        NextQuestionButton.IsEnabled = currentQuestionIndex < QuestionList.Count - 1;

        QuestionTextBlock.Text = QuestionList[currentQuestionIndex].Content;

        DrawRect.DrawRectangleOnCanvas(QuestionList[currentQuestionIndex].Items, Colors.Gray);

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
            if (q.SelectedSort != null)
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
            string selectedSortName = selectedRadioButton.Tag.ToString()!;
            SortTypes sortTypes = SortUtils.GetSortType(selectedSortName);
            QuestionList[currentQuestionIndex].SelectedSort = SortUtils.InstantiateSort(sortTypes, QuestionList[currentQuestionIndex].Items, new DrawRectangle(new Canvas()));
            _ = QuestionList[currentQuestionIndex].SelectedSort!.BeginSortingAsync();
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
        int totalSteps = QuestionList.Sum(static q => q.SelectedSort != null ? q.SelectedSort.Step : 0);
        int score = Utils.Utils.CalcScore(totalSteps, QuestionList.Count, CountAnswer());

        if (App.User != null && App.UserSvc != null)
        {
            App.User.SetScore(score);
            await App.UserSvc.UpdateScoreAsync(App.User);

            WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
            {
                Title = "Info",
                Content = $"CONGRATS! YOU GOT {score} points!\n\nChoose 'OK' button to navigate to the ScoreBoard \nand 'Close' button to navigate to the Homepage.",
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
                Content = $"CONGRATS! YOU GOT {score} points!\n\nChoose 'OK' button to navigate to the Homepage \nand 'Close' button to answer quiz again.",
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
            RadioButtons.Add(radioButton);
        }
        Keyboard.Focus(MultipleChoiceWrapPanel);
        DrawRect.DrawRectangleOnCanvas(QuestionList[currentQuestionIndex].Items, Colors.Gray);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.A:
                RadioButtons[0].IsChecked = true;
                break;
            case Key.B:
                RadioButtons[1].IsChecked = true;
                break;
            case Key.C:
                RadioButtons[2].IsChecked = true;
                break;
            case Key.D:
                RadioButtons[3].IsChecked = true;
                break;
            case Key.E:
                RadioButtons[4].IsChecked = true;
                break;
            case Key.F:
                RadioButtons[5].IsChecked = true;
                break;
            case Key.G:
                RadioButtons[6].IsChecked = true;
                break;
            case Key.H:
                RadioButtons[7].IsChecked = true;
                break;
            case Key.I:
                RadioButtons[8].IsChecked = true;
                break;
            case Key.J:
                RadioButtons[9].IsChecked = true;
                break;
            case Key.K:
                RadioButtons[10].IsChecked = true;
                break;
            case Key.L:
                RadioButtons[11].IsChecked = true;
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