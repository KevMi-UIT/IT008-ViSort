using System.Windows;
using ViSort.Pages;

namespace ViSort.Windows;

public partial class MainWindow : Window
{
    public static WpfUiControls.NavigationView RootNavigationView { get; private set; } = new();

    public static bool IsDarkMode { get; set; } = false;

    public MainWindow()
    {
        InitializeComponent();
        RootNavigationView = RootNavigation;

        Loaded += (sender, args) =>
        {
            RootNavigation.Navigate(typeof(HomePage));
        };
    }
}