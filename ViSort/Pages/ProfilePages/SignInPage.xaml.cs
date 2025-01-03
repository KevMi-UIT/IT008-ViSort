﻿using System.Windows;
using System.Windows.Controls;
using ViSort.Models;
using ViSort.Pages.ProfilePages;
using ViSort.Windows;
using static ViSort.Exceptions.UserExceptions;

namespace ViSort.Pages;

public partial class SignInPage : Page
{
    public SignInPage()
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
            await App.UserSvc!.SignInAsync(User);
            App.User = User;
            MainWindow.RootNavigationView.Navigate(typeof(ProfilePage));
        }
        catch (PasswordDoesNotMatch)
        {
            await new WpfUiControls.MessageBox
            {
                Title = "Sign in error.",
                Content = "Password is incorrect."
            }.ShowDialogAsync();
        }
        catch (UserNotFound)
        {
            await new WpfUiControls.MessageBox
            {
                Title = "Sign in error.",
                Content = "Username not found. Recheck spellings or create new account."
            }.ShowDialogAsync();
        }
        SubmitButton.IsEnabled = true;
    }

    private void SignUpLink_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new SignUpPage());
    }
}