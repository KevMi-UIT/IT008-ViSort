using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using ViSort.Sorts;
using ViSort.Utils;

namespace ViSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> list;
        private int threadDelay = 100;
        private GenRandomList.RandomGenTypes selectedType = new GenRandomList.RandomGenTypes();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectSortAlgorithms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedAlgorithm = (ComboBoxItem)SelectSortAlgorithms.SelectedItem;
            if (selectedAlgorithm != null)
            {
                AlgorithmName.Content = $"{selectedAlgorithm.Content}";
            }
        }

        private bool isPlaying = false; // Biến theo dõi trạng thái play/stop

        private async void PlayStopButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 1;

            if (isPlaying)
            {
                // Nếu đang ở trạng thái "Play", chuyển sang trạng thái "Stop"
                Display.IsEnabled = false;
                Display.Opacity = 0;
                SortType.IsEnabled = true;
                SortType.Opacity = 1;
                Title.Opacity = 1;
                PlayStopButton.Content = "Play"; // Thay đổi nội dung nút thành "Play"
            }
            else
            {
                // Nếu đang ở trạng thái "Stop", chuyển sang trạng thái "Play"
                Display.IsEnabled = true;
                Display.Opacity = 100;
                SortType.IsEnabled = false;
                SortType.Opacity = 0;
                Title.Opacity = 0;
                PlayStopButton.Content = "Stop"; // Thay đổi nội dung nút thành "Stop"
            }

            // Đảo ngược trạng thái isPlaying
            isPlaying = !isPlaying;
        }


        private void JumpToEnd_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Random rd = new Random();
            int length = rd.Next(20, 100); // Số lượng phần tử trong danh sách

            List<int> list = GenRandomList.GenList(length, selectedType);

            // Hiển thị danh sách trong TextBox hoặc ListBox, ví dụ:
            CreateResult.Text = string.Join(", ", list);
        }

        private void NormalGenerate_Checked(object sender, RoutedEventArgs e)
        {
            selectedType = GenRandomList.RandomGenTypes.Normal;
        }

        private void SortedGenerate_Checked(object sender, RoutedEventArgs e)
        {
            selectedType = GenRandomList.RandomGenTypes.Sorted;
        }

        private void SortedReverseGenerate_Checked(object sender, RoutedEventArgs e)
        {
            selectedType = GenRandomList.RandomGenTypes.SortedReverse;
        }

        private void NearlySortedGenerate_Checked(object sender, RoutedEventArgs e)
        {
            selectedType = GenRandomList.RandomGenTypes.NearlySorted;
        }

        private void MirrorGenerate_Checked(object sender, RoutedEventArgs e)
        {
            selectedType = GenRandomList.RandomGenTypes.Mirror;
        }

        private void SpeedChange_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CurrentSpeed != null && !CurrentSpeed.IsFocused) // Chỉ cập nhật nếu TextBox không được chọn
            {
                threadDelay = (int)e.NewValue;
                CurrentSpeed.Text = threadDelay.ToString();
            }
        }

        private void CurrentSpeed_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateThreadDelayFromTextBox();
        }

        private void CurrentSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateThreadDelayFromTextBox();
                Keyboard.ClearFocus();
                
            }
        }

        // Hàm cập nhật threadDelay từ TextBox khi người dùng nhập giá trị
        private void UpdateThreadDelayFromTextBox()
        {
            int MIN = (int)SpeedChange.Minimum;
            int MAX = (int)SpeedChange.Maximum;

            if (int.TryParse(CurrentSpeed.Text, out int value))
            {
                // Kiểm tra giá trị có nằm trong giới hạn Slider không
                if (value >= MIN && value <= MAX)
                {
                    threadDelay = value;
                    SpeedChange.Value = value; // Cập nhật Slider
                    CurrentSpeed.Text = threadDelay.ToString();
                }
                else
                {
                    MessageBox.Show($"Please enter a value between {MIN} and {MAX}.");
                    CurrentSpeed.Text = threadDelay.ToString(); // Reset lại TextBox với giá trị hiện tại
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid integer.");
                CurrentSpeed.Text = threadDelay.ToString();
            }
        }

    }
}
