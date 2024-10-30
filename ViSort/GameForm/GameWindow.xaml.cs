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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            DisplayCurrentQuestion();
        }
        private List<string> QuestionList = new List<string>();
        private int currentQuestionIndex = 0;

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
            if (currentQuestionIndex >= 0 && currentQuestionIndex < QuestionList.Count)
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }
    }
}
