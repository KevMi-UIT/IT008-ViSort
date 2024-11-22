using System;
using System.Collections.Generic;
using System.Diagnostics;
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