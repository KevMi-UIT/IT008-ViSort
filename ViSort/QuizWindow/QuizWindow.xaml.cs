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
using System.Windows.Shapes;
using ViSort.QuizWindow;
using ViSort.Sorts;
using Wpf.Ui.Controls;
using static ViSort.Utils.Utils;

namespace ViSort.QuizWindow;

/// <summary>
/// Interaction logic for QuizWindow.xaml
/// </summary>
public partial class QuizWindow : Window
{
    private List<QuizQuestion> QuestionList = [];
    private int currentQuestionIndex = 0;
    public QuizWindow()
    {
        foreach (var question in QuizQuestionsList.QUESTIONS)
        {
            var q = new QuizQuestion(string.Join(", ", question));
            QuestionList.Add(q);
        }
        Utils.Utils.Randomize(QuestionList);
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
        int score = CalcScore(0, QuestionList.Count, CountAnswer());

        if (App.User != null && App.UserSvc != null)
        {
            App.User.SetScore(score);
            await App.UserSvc.UpdateScoreAsync(App.User);
        }
        this.Close();
    }
}