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
            NavigationService.Navigate(new ProfilePage());
        }
        catch (PasswordDoesNotMatch)
        {
            await new WpfUiControl.MessageBox
            {
                Title = "Lỗi đăng nhập",
                Content = "Mật khẩu không chính xác",
                PrimaryButtonText = "OK"
            }.ShowDialogAsync();
        }
        SubmitButton.IsEnabled = true;
    }

    // skipcq: CS-R1005
    private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameTextbox.Text) && string.IsNullOrWhiteSpace(Password_Passwordbox.Password))
        {
            WpfUiControl.MessageBoxResult result = await new WpfUiControl.MessageBox
            {
                Title = "Warning",
                Content = "Chưa có thông tin đăng nhập. Tiếp tục mà không đăng nhập?",
                PrimaryButtonText = "OK",
            }.ShowDialogAsync();
            if (result == WpfUiControl.MessageBoxResult.Primary)
            {
                e.Cancel = true;
            }
        }
    }