using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViSort.Database;
using ViSort.Models;
using ZstdSharp;

namespace ViSort;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static IConfiguration Config { get; private set; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    internal static UserModel? User { get; set; } = null;
    internal static UserService? UserSvc { get; set; } = null;

    public App()
    {
        Task.Run(() =>
        {
            try
            {
                UserSvc = new();
            }
            finally
            {
            }
        });
    }

    public static void EstablishConnection()
    {
        try
        {
            UserSvc ??= new();
        }
        catch (Exception)
        {
            MessageBox.Show("Không thể khởi tạo kết nối đến máy chủ cơ sở dữ liệu.", "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}