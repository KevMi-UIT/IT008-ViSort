using System.Windows.Controls;
using System.Windows.Navigation;

namespace ViSort.Pages.ProfilePages;

public partial class ProfileSwitchPage : Page
{
    public ProfileSwitchPage()
    {
        InitializeComponent();
        Loaded += (sender, args) =>
        {
            if (App.User == null)
            {
                NavigationService.Navigate(new AuthPage());
            }
            else
            {
                NavigationService.Navigate(new ProfilePage());
            }
        };
    }
}