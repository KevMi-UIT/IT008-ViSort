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

namespace ViSort.Pages.ProfilePages;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.User == null)
        {
            var loginWindow = new UserFillForm();
            loginWindow.ShowDialog();

            if (App.User != null)
            {
                await App.UserSvc!.AuthUserAsync(App.User!);
                UpdateProfile(true);
            }
        }
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

    private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void UpdateProfile(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            LoginButton.Visibility = Visibility.Collapsed;
            LogoutButton.Visibility = Visibility.Visible;
            DeleteProfileButton.Visibility = Visibility.Visible;
            UsernameValue.Text = App.User!.Username;
            ScoreValue.Text = App.User.Score.ToString();
        }
        else
        {
            LoginButton.Visibility = Visibility.Visible;
            LogoutButton.Visibility = Visibility.Collapsed;
            DeleteProfileButton.Visibility = Visibility.Collapsed;
            UsernameValue.Text = "Chưa có thông tin";
            ScoreValue.Text = "Chưa có thông tin";
        }
    }
}