﻿using System.Windows;
using System.Windows.Controls;
using ViSort.Models;
using ViSort.Windows;
using static ViSort.Exceptions.UserExceptions;

namespace ViSort.Pages.ProfilePages;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage()
    {
        InitializeComponent();
        EditUsernameTextBox.Text = App.User!.Username;
        EditPasswordTextBox.Text = App.User.Password;
        ScoreValue.Text = App.User.Score.ToString();
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.User != null)
        {
            App.User = null;
            MainWindow.RootNavigationView.Navigate(typeof(SignInPage));
        }
        else
        {
            LogoutButton.IsEnabled = false;
        }
    }

    private async void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.User != null)
        {
            await App.UserSvc!.DeleteUserAsync(App.User);
            App.User = null;
        }
        MainWindow.RootNavigationView.Navigate(typeof(ProfileSwitchPage));
    }

    private async void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
    {
        string newUsername = EditUsernameTextBox.Text;
        string newPassword = EditPasswordTextBox.Text;
        UserModel NewUserModel = new(newUsername, newPassword);
        if (!UserUtils.ValidateUsername(newUsername))
        {
            WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
            {
                Title = "Error",
                Content = "Invalid Username!",
            }.ShowDialogAsync();
        }
        else if (!UserUtils.ValidatePassword(newPassword))
        {
            WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
            {
                Title = "Error",
                Content = "Invalid Password!",
            }.ShowDialogAsync();
        }
        else
        {
            try
            {
                await App.UserSvc!.ChangeUserProfileAsync(App.User!, NewUserModel);
                WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
                {
                    Title = "Success",
                    Content = "Profile updated successfully!",
                }.ShowDialogAsync();
            }
            catch (UserNoChanges)
            {
                WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
                {
                    Title = "Info",
                    Content = "No modification on user.",
                }.ShowDialogAsync();
            }
            catch (UserAlreadyExists)
            {
                WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
                {
                    Title = "Error",
                    Content = "User already exists.",
                }.ShowDialogAsync();
            }
            catch (Exception ex)
            {
                WpfUiControls.MessageBoxResult result = await new WpfUiControls.MessageBox
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                }.ShowDialogAsync();
            }
        }
        App.User = NewUserModel;
    }
}