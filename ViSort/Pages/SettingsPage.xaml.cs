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
using Wpf.Ui.Appearance;

namespace ViSort.Pages;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        switch (ApplicationThemeManager.GetAppTheme())
        {
            case ApplicationTheme.Light:
                LightRadioButton.IsChecked = true;
                break;
            case ApplicationTheme.Dark:
                DarkRadioButton.IsChecked = true;
                break;
            default:
                throw new ArgumentException("Invalid theme");
        }
    }

    private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            ApplicationTheme theme = radioButton.Tag.ToString() switch
            {
                "Light" => ApplicationTheme.Light,
                "Dark" => ApplicationTheme.Dark,
                _ => ApplicationTheme.Unknown
            };
            ApplicationThemeManager.Apply(theme);
        }
    }
}