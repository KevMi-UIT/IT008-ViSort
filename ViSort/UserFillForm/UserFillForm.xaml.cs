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
using Windows.System;
using static ViSort.Database.UserExceptions;


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
        UserModel User = new(UsernameTextbox.Text, password);

        if (!UserUtils.ValidateUsername(User.Username))
        {
            ValidateUsernameTextBlock.Visibility = Visibility.Visible;
            return;
        }
        ValidateUsernameTextBlock.Visibility = Visibility.Hidden;

        if (!UserUtils.ValidatePassword(password))
        {
            ValidatePasswordTextBlock.Visibility = Visibility.Visible;
            return;
        }
        ValidatePasswordTextBlock.Visibility = Visibility.Hidden;
        SubmitButton.IsEnabled = false;

        try
        {
            await App.UserSvc!.AuthUserAsync(User);
            App.User = User;
        }
        catch (PasswordDoesNotMatch)
        {
            MessageBox.Show("Mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (UserNotFound)
        {
            MessageBox.Show("Người dùng không tồn tại.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            SubmitButton.IsEnabled = true;
            if (App.User == null)
            {
                MessageBox.Show("Đăng nhập không thành công.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Close();
            }
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameTextbox.Text) && string.IsNullOrWhiteSpace(Password_Passwordbox.Password))
        {
            var result = MessageBox.Show("Chưa có thông tin đăng nhập. Tiếp tục mà không đăng nhập?",
                                         "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }

}