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
using ViSort.Sorts;

namespace ViSort.QuizForm
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private class Question
        {
            public string Content { get; set; }
            public List<SortTypes> Answer { get; set; }
            public SortTypes SelectedAnswer { get; set; }
            public bool isSelected = false;

            public Question(string content, List<SortTypes> answers)
            {
                Content = content;
                Answer = answers;
                SelectedAnswer = default;
            }
        }

        List<SortTypes> ans = Enum.GetValues(typeof(SortTypes)).Cast<SortTypes>().ToList();

        public QuizWindow()
        {
            InitializeComponent();
            LoadAndShuffleQuestions();
            DisplayCurrentQuestion();
        }

        private List<Question> QuestionList = new List<Question>();
        private int currentQuestionIndex = 0;

        private void LoadAndShuffleQuestions()
        {
            var filePath = "QuizForm/question.txt";

            // Đọc tất cả câu hỏi từ tệp
            var questions = File.ReadAllLines(filePath).ToList();

            // Xáo trộn danh sách câu hỏi
            var random = new Random();
            questions = questions.OrderBy(x => random.Next()).ToList();

            // Thêm câu hỏi vào WrapPanel
            foreach (var q in questions)
            {
                QuestionList.Add(new Question(q, ans));
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < QuestionList.Count)
            {
                QuestionTextBlock.Text = QuestionList[currentQuestionIndex].Content;
            }
        }
        private void PreviousQuestion(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex > 0 && currentQuestionIndex <= QuestionList.Count)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
            }

        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < QuestionList.Count - 1)
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }

        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            // Xác định nút radio nào đã được chọn
            var selectedRadioButton = sender as RadioButton;

            if (selectedRadioButton != null)
            {
                string selectedAnswer = selectedRadioButton.Content.ToString();
                // Thực hiện hành động với câu trả lời đã chọn
                MessageBox.Show($"Bạn đã chọn: {selectedAnswer}");
            }
        }

    }
}
