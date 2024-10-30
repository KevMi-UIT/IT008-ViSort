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

namespace ViSort.GameForm
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            LoadAndShuffleQuestions();
            DisplayCurrentQuestion();
        }
        private List<string> QuestionList = new List<string>();
        private int currentQuestionIndex = 0;

        private void LoadAndShuffleQuestions()
        {
            var filePath = "GameForm/question.txt";

            // Đọc tất cả câu hỏi từ tệp
            var questions = File.ReadAllLines(filePath).ToList();

            // Xáo trộn danh sách câu hỏi
            var random = new Random();
            questions = questions.OrderBy(x => random.Next()).ToList();

            // Thêm câu hỏi vào WrapPanel
            foreach (var question in questions)
            {
                QuestionList.Add(question);
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < QuestionList.Count)
            {
                QuestionTextBlock.Text = QuestionList[currentQuestionIndex];
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
