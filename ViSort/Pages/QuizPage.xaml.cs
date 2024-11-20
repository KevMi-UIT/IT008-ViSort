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
using ViSort.Models;
using ViSort.Models.SortModels;
using ViSort.Utils;

namespace ViSort.Pages;

/// <summary>
/// Interaction logic for QuizPage.xaml
/// </summary>
public partial class QuizPage : Page
{
    private readonly List<QuizModel> QuestionList = [];
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
        var selectedRadioButton = sender as RadioButton;
        if (selectedRadioButton != null)
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
        }
    }
}