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
using System.Windows.Shapes;

namespace ViSort.ScoreBoard;
/// <summary>
/// Interaction logic for ScoreBoard.xaml
/// </summary>
public partial class ScoreBoard : Window
{
    public ScoreBoard()
    {
        InitializeComponent();
        _ = SetDataItems();
    }

    private async Task SetDataItems()
    {
        ScoreData.ItemsSource = await App.UserSvc!.GetAllUsersResultAsync();
    }
}