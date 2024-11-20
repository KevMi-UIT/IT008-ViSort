using System;
using System.Collections.Generic;
using System.Configuration;
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
using ViSort.Models;

namespace ViSort.Pages;

public partial class ScoreBoardPage : Page
{
    public List<UserModel> Users { set; get; } = [];
    public ScoreBoardPage()
    {
        InitializeComponent();
        if (!(App.EstablishDBConnectionAsync().Result))
        {
            return;
        }
        _ = GetListViewScoreAsync();
    }

    private async Task GetListViewScoreAsync()
    {
        ListViewScore.ItemsSource = await App.UserSvc!.GetAllUsersResultAsync();
    }
}