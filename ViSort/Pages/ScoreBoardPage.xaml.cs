using System.Windows.Controls;
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