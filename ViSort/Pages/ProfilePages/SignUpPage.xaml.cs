using System.Windows;
using System.Windows.Controls;
using ViSort.Models;
using ViSort.Pages.ProfilePages;
using ViSort.Windows;
using static ViSort.Exceptions.UserExceptions;

namespace ViSort.Pages;

public partial class SignUpPage : Page
{
    public SignUpPage()
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
        string confirmPassword = PasswordConfirm_Passwordbox.Password;
        UserModel User = new(UsernameTextbox.Text, password);

        if (!UserUtils.ValidateUsername(User.Username))
        {
            ValidateUsernameTextBlock.Visibility = Visibility.Visible;
            return;
        }
        ValidateUsernameTextBlock.Visibility = Visibility.Hidden;

        if (!UserUtils.ValidatePassword(password))
        {
            ValidatePasswordTextBlock.Text = "Mật khẩu không đúng định dạng. Vui lòng nhập lại";
            ValidatePasswordTextBlock.Visibility = Visibility.Visible;
            return;
        }

        if (password != confirmPassword)
        {
            ValidatePasswordTextBlock.Text = "Mật khẩu xác nhận không khớp. Vui lòng kiểm tra lại.";
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
            await App.UserSvc!.SignUpAsync(User);
            App.User = User;
            MainWindow.RootNavigationView.Navigate(typeof(ProfilePage));
        }
        catch (UserAlreadyExists)
        {
            await new WpfUiControls.MessageBox
            {
                Title = "Lỗi đăng kí",
                Content = "Username đã tồn tại."
            }.ShowDialogAsync();
        }
        SubmitButton.IsEnabled = true;
    }

    private void SignUpLink_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.RootNavigationView.Navigate(typeof(SignInPage));
    }
}