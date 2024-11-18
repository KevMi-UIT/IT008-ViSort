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
        UpdateProfileButton.IsEnabled = false;
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
            UsernameValue.Text = App.User!.Username;
            ScoreValue.Text = App.User.Score.ToString();
        }
        else
        {
            UsernameValue.Text = "Unknown";
            ScoreValue.Text = "Unknown";
        }
    }

    private void EditProfileButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.User != null)
        {
            EditUsernameTextBox.Select(EditUsernameTextBox.Text.Length, 0);
            EditPasswordTextBox.Select(EditPasswordTextBox.Text.Length, 0);
        }
    }

    private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
    {

    }
    private void EditUsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}