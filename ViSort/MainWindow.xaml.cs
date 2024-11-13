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