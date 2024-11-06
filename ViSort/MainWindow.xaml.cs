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

namespace ViSort
{
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

            var UserFillForm = new UserFillForm();
            UserFillForm.ShowDialog();
        }


    }
}
