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
using ViSort.Database;
using ViSort.Models;
using ViSort.Windows;
using static ViSort.Database.UserService;

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
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.User != null)
        {
            App.User = null;
            UpdateProfile(false);
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
            UpdateProfile(false);
        }
        MainWindow.RootNavigationView.Navigate(typeof(ProfilePage));
    }
    private void UpdateProfile(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            EditUsernameTextBox.Text = App.User!.Username;
            EditPasswordTextBox.Text = App.User.Password;
            ScoreValue.Text = App.User.Score.ToString();
        }
        else
        {
            EditUsernameTextBox.Text = "Unknown";
            EditPasswordTextBox.Text = "Unknown";
            ScoreValue.Text = "Unknown";
            UpdateProfileButton.IsEnabled = false;
        }
    }

    private async void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
    {
        //UpdateProfileButton.IsEnabled = true;
        string newUsername = EditUsernameTextBox.Text;
        string newPassword = EditPasswordTextBox.Text;
        UserModel NewUserModel = new(newUsername, newPassword);
        try
        {
            await App.UserSvc!.ChangeUserProfileAsync(App.User!, NewUserModel);
        }
        catch (Exception)
        {

        }
        MainWindow.RootNavigationView.Navigate(typeof(ProfilePage));
    }
}