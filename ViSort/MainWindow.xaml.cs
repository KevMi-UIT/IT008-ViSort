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
using Windows.System;
using Wpf.Ui;

namespace ViSort;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += (sender, args) =>
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(
                this,                                 // Window class
                WpfUiControl.WindowBackdropType.Auto, // Background type
                true                                  // Whether to change accents automatically
            );
            RootNavigation.Navigate(typeof(HomePage));
        };
    }
}