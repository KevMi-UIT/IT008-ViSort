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


namespace ViSort;

/// <summary>
/// Interaction logic for UserFillForm.xaml
/// </summary>
public partial class UserFillForm : Window
{
    public UserFillForm()
    {
        InitializeComponent();
        App.UserSvc ??= new();
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {

        string password = Password_Passwordbox.Password;
        UserModel User = new(Username_Textbox.Text, password);

        if (!UserUtils.ValidateUsername(User.Username))
        {
            ValidateUsername_TextBlock.Visibility = Visibility.Visible;
            return;
        }
        ValidateUsername_TextBlock.Visibility = Visibility.Hidden;

        if (!UserUtils.ValidatePassword(password))
        {
            ValidatePassword_TextBlock.Visibility = Visibility.Visible;
            return;
        }
        ValidatePassword_TextBlock.Visibility = Visibility.Hidden;

        SubmitButton.IsEnabled = false;

        if (App.User != null)
        {
            await App.UserSvc!.AuthUserAsync(User);
            App.User = User;
        }
        SubmitButton.IsEnabled = true;
        this.Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        string password = Password_Passwordbox.Password;
        if (Username_Textbox.Text == "" && password == "")
        {
            MessageBox.Show("Chưa có thông tin đăng nhập. Tiếp tục mà không đăng nhập?", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}