using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZstdSharp;

namespace ViSort;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IConfiguration? Config { get; private set; }

    public App()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
}