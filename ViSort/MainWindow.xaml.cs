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
using ViSort.Sorts;
using ViSort.Utils;

namespace ViSort;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int threadDelay;
    private int elementCount = 100;
    private SortTypes selectedSortType = default;
    private RandomGenTypes selectedGenType = default;
    public MainWindow()
    {
        InitializeComponent();
        BubbleButton.IsChecked = true;
        NormalGenButton.IsChecked = true;
    }

    // Sự kiện cho RadioButton của thuật toán sắp xếp
    private void SortAlgorithm_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            selectedSortType = radioButton.Content.ToString() switch
            {
                "Bubble Sort" => SortTypes.Bubble,
                "Bucket Sort" => SortTypes.Bucket,
                "Counting Sort" => SortTypes.Counting,
                "Selection Sort" => SortTypes.Selection,
                "Insertion Sort" => SortTypes.Insertion,
                "Merge Sort" => SortTypes.Merge,
                "Quick Sort" => SortTypes.Quick,
                "Heap Sort" => SortTypes.Heap,
                "Radix Sort" => SortTypes.Radix,
                "Shell Sort" => SortTypes.Shell,
                "Tim Sort" => SortTypes.Tim,
                "Tree Sort" => SortTypes.Tree,
                _ => throw new ArgumentException("Unsupported sorting algorithm selected")
            };
            UpdateSelectedSection();
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

            list = GenRandomList.GenList(length, selectedType);

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

    // Sự kiện cho RadioButton của phương pháp tạo mảng
    private void ArrayGenerationMethod_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            selectedGenType = radioButton.Content.ToString() switch
            {
                "Normal" => RandomGenTypes.Normal,
                "Sorted" => RandomGenTypes.Sorted,
                "Sorted Reverse" => RandomGenTypes.SortedReverse,
                "Nearly Sorted" => RandomGenTypes.NearlySorted,
                "Mirror" => RandomGenTypes.Mirror,
                _ => throw new ArgumentException("Unsupported random")
            };
            UpdateSelectedSection();
        }
    }

    // Cập nhật TextBlock với thông tin hiện tại
    private void UpdateSelectedSection()
    {
        SelectedSectionTextBlock.Text = $"Algorithm: {selectedSortType}, Array: {selectedGenType}";
    }


    private void ElementCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // Cập nhật TextBox khi Slider thay đổi
        if (ElementCountTextBox != null && !ElementCountTextBox.IsFocused)
        {
            threadDelay = (int)e.NewValue;
            ElementCountTextBox.Text = threadDelay.ToString();
        }
    }

    private void ElementCountTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UpdateSliderFromTextBox();
    }

    private void ElementCountTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        // Cập nhật khi người dùng nhấn Enter trong TextBox
        if (e.Key == Key.Enter)
        {
            UpdateSliderFromTextBox();
        }
    }

    private void UpdateSliderFromTextBox()
    {
        // Chuyển đổi TextBox thành số và cập nhật Slider nếu hợp lệ
        if (int.TryParse(ElementCountTextBox.Text, out int value) && value >= ElementCountSlider.Minimum && value <= ElementCountSlider.Maximum)
        {
            threadDelay = value;
            ElementCountSlider.Value = value;
        }
        else
        {
            // Thông báo lỗi nếu giá trị không hợp lệ và reset về threadDelay
            MessageBox.Show("Please enter a valid integer within the range.");
            ElementCountTextBox.Text = threadDelay.ToString();
        }
    }

    private void ElementCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Kiểm tra nếu ký tự nhập vào là số
        e.Handled = !IsTextNumeric(e.Text);
    }

    private bool IsTextNumeric(string text)
    {
        // Kiểm tra nếu chuỗi chỉ chứa ký tự số
        return int.TryParse(text, out _);
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        this.Hide();
        // Tạo và hiển thị cửa sổ DisplayWindow
        DisplayWindow displayWindow = new(elementCount, threadDelay, selectedSortType!, selectedGenType!);
        displayWindow.Closed += (s, e) => Show();
        displayWindow.Show();
    }
}