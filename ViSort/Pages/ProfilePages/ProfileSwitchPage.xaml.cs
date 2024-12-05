using System.Windows.Controls;
using System.Windows.Navigation;
using ViSort.Windows;

namespace ViSort.Pages.ProfilePages;

public partial class ProfileSwitchPage : Page
{
    public ProfileSwitchPage()
    {
        InitializeComponent();
        Loaded += async (sender, args) =>
        {
            if (App.UserSvc == null)
            {
                await new WpfUiControls.MessageBox
                {
                    Title = "Connection Error",
                    Content = "Cannot connect to database. Go to Home now."
                }.ShowDialogAsync();
                MainWindow.RootNavigationView.Navigate(typeof(HomePage));
            }
            if (App.User == null)
            {
                NavigationService.Navigate(new SignInPage());
            }
            else
            {
                NavigationService.Navigate(new ProfilePage());
            }
        };
    }
}