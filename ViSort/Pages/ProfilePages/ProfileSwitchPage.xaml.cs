﻿using System;
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