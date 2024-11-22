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
using ViSort.Pages.ProfilePages;
using ViSort.Windows;
using static ViSort.Exceptions.UserExceptions;

namespace ViSort.Pages;

public partial class AuthPage : Page
{
    public AuthPage()
    {
        if (!App.EstablishDBConnectionAsync().Result)
        {
            return;
        }
        InitializeComponent();
        UsernameTextbox.Focus();
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
            if (App.UserSvc == null)
            {
                throw new InvalidOperationException("User service is not initialized.");
            }
            await App.UserSvc!.AuthUserAsync(User);
            App.User = User;
            MainWindow.RootNavigationView.Navigate(typeof(ProfilePage));
        }
        catch (PasswordDoesNotMatch)
        {
            await new WpfUiControls.MessageBox
            {
                Title = "Lỗi đăng nhập",
                Content = "Mật khẩu không chính xác"
            }.ShowDialogAsync();
        }
        SubmitButton.IsEnabled = true;
    }
}