using System;
using System.Collections.Generic;
using System.IO;
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
using ViSort.Pages;

namespace ViSort.Windows;

public partial class MainWindow : Window
{
    public static WpfUiControl.NavigationView RootNavigationView = new();
    public MainWindow()
    {
        InitializeComponent();
        RootNavigationView = RootNavigation;

        Loaded += (sender, args) =>
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(
                this,
                WpfUiControl.WindowBackdropType.None,
                true
            );
            RootNavigation.Navigate(typeof(HomePage));
        };
    }
}