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
using System.Windows.Shapes;
using ViSort.Models;

namespace ViSort
{
    /// <summary>
    /// Interaction logic for UserFillForm.xaml
    /// </summary>
    public partial class UserFillForm : Window
    {
        public UserFillForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string password = showPasswordCheckBox.IsChecked == true ? PasswordTextBox.Text : PasswordBox.Password;
            UserModel User = new UserModel(UserName.Text, password, 0);
            this.Close();
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
        }

        private void Closing_window(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string password = showPasswordCheckBox.IsChecked == true ? PasswordTextBox.Text : PasswordBox.Password;
            if (UserName.Text == "" && password == "")
                MessageBox.Show("Chưa có thông tin đăng nhập. \nTiếp tục mà không đăng nhập?", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            var QuizWindow = new QuizForm.QuizWindow();
            QuizWindow.ShowDialog();
            return;
        }
    }
}

