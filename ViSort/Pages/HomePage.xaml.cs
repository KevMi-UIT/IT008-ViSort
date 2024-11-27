using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ViSort.Windows;
using static ViSort.Utils.Utils;

namespace ViSort.Pages;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        if (MainWindow.IsDarkMode)
        {
            BannerImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Logo/visort_full_logo_dark.png", UriKind.Absolute));
        }
    }

    private void HyperLinkGithub_Click(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is Hyperlink link)
        {
            OpenLink(link.NavigateUri.ToString());
        }
    }
}