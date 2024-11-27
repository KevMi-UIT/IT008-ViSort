using Microsoft.Extensions.Configuration;
using System.Windows;
using ViSort.Database;
using ViSort.Models;

namespace ViSort;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IConfiguration Config { get; private set; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    public static UserModel? User { get; set; } = null;
    public static UserService? UserSvc { get; set; } = null;
    public static readonly List<List<int>> QUIZ_LIST = [
    //[12, 45, 7, 89, 23, 3, 99, 54, 31, 20, 76, 18, 67, 12, 34, 50, 29, 41], //quick, tree?
    //[1, 2, 3, 4, 5, 10, 20, 30, 40, 50, 60, 70, 5, 10, 15, 20, 25, 30],
    //[1, 2, 4, 3, 5, 10, 12, 11, 14, 15, 20, 22, 21, 24, 25, 26, 27],
    //[1, 2, 3, 4, 3, 2, 1, 10, 20, 30, 40, 30, 20, 10, 5, 10, 15, 20, 15, 10, 5], //counting
    //[5, 4, 3, 2, 1, 70, 60, 50, 40, 30, 20, 10, 30, 25, 20, 15, 10, 5],
    //[8, 2, 19, 34, 12, 56, 40, 9, 28, 55, 31, 6, 14, 22, 8, 39, 17], //merge, heap
    //[3, 6, 9, 12, 15, 2, 4, 6, 8, 10, 12, 14, 16, 100, 200, 300, 400, 500], //quick, merge, tim
    //[1, 3, 2, 4, 5, 9, 11, 10, 13, 15, 50, 49, 51, 53, 55], //insertion
    //[1, 2, 3, 4, 5, 4, 3, 2, 1, 7, 14, 21, 28, 21, 14, 7, 11, 22, 33, 44, 33, 22, 11],
    //[25, 20, 15, 10, 5, 15, 12, 9, 6, 3, 100, 90, 80, 70, 60, 50],
    //[18, 29, 14, 35, 44, 22, 30, 55, 45, 20, 70, 10, 5, 15, 25, 20, 30],
    //[2, 1, 3, 4, 5, 4, 6, 8, 7, 10, 12, 15, 14, 18, 20],
    //[3, 6, 9, 12, 9, 6, 3, 5, 10, 15, 20, 15, 10, 5, 2, 4, 6, 8, 6, 4, 2],
    //[40, 30, 20, 10, 0, 9, 8, 7, 6, 5, 4, 3, 25, 20, 15, 10, 5],
    //[13, 27, 18, 36, 45, 56, 64, 68, 70, 72, 76, 89, 115, 118, 127], //buble, insertion, tim
    [329, 457, 657, 839, 436, 720, 355, 400, 452, 974, 255, 104, 832], //radix
    [1, 2, 3, 4, 5, 10, 27, 33, 46, 57, 68, 79, 87 , 89, 105, 120, 125, 230], //insertion, tim, shell
    [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1], //counting
    [17, 3, 14, 8, 6, 20, 2, 12, 15, 19, 5, 11, 9, 18, 7, 1], //bucket
    [43, 32, 23, 20, 18, 15, 11, 10, 9, 8, 7, 5, 4, 3, 1, 0], //merge, heap
];

    public App()
    {
        Task.Run(static () =>
        {
            try
            {
                UserSvc = new();
            }
            catch (Exception) // skipcq: CS-R1008
            {
            }
        });
    }

    public static async Task<Boolean> EstablishDBConnectionAsync()
    {
        try
        {
            UserSvc ??= new();
            return true;
        }
        catch (Exception) // skipcq: CS-R1008
        {
            await new WpfUiControls.MessageBox
            {
                Title = "Lỗi kết nối",
                Content = "Không thể khởi tạo kết nối đến máy chủ cơ sở dữ liệu."
            }.ShowDialogAsync();
            return false;
        }
    }
}