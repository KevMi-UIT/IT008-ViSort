using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using ViSort.Sorts;
using ViSort.Utils;

namespace ViSort;

/// <summary>
/// Interaction logic for DisplayWindow.xaml
/// </summary>
public partial class DisplayWindow : Window
{
    private int threadDelay;
    private readonly int elementCount;
    private readonly SortTypes selectedSortType;
    private readonly RandomGenTypes selectedArrayGenerationMethod;
    private List<int> mylist = [];
    private readonly BaseSort selectedSort;

    internal DisplayWindow(int _elementCount, int _threadDelay, SortTypes _selectedSortAlgorithm, RandomGenTypes _selectedArrayGenerationMethod)
    {
        InitializeComponent();
        elementCount = _elementCount;
        threadDelay = _threadDelay;
        selectedSortType = _selectedSortAlgorithm;
        selectedArrayGenerationMethod = _selectedArrayGenerationMethod;
        selectedSort = selectedSortType switch
        {
            SortTypes.Bubble => new BubbleSort(),
            SortTypes.Bucket => throw new NotImplementedException(),
            SortTypes.Counting => throw new NotImplementedException(),
            SortTypes.Selection => throw new NotImplementedException(),
            SortTypes.Insertion => throw new NotImplementedException(),
            SortTypes.Merge => throw new NotImplementedException(),
            SortTypes.Quick => throw new NotImplementedException(),
            SortTypes.Heap => throw new NotImplementedException(),
            SortTypes.Radix => throw new NotImplementedException(),
            SortTypes.Shell => throw new NotImplementedException(),
            SortTypes.Tim => throw new NotImplementedException(),
            SortTypes.Tree => throw new NotImplementedException(),
            _ => throw new NotImplementedException("Sort hasn't been implemented")
        };
        mylist = GenRandomList.GenList(_elementCount, _selectedArrayGenerationMethod);
    }

    //private void SetUpFormData(string _setModifier)
    //{
    //    SortTypes sortType = (SortTypes)Enum.Parse(typeof(SortTypes), selectedSortType);
    //    //Cập nhật cách sử dụng SortTypes
    //}

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // Cập nhật TextBox khi Slider thay đổi
        if (SpeedTextBox != null && !SpeedTextBox.IsFocused)
        {
            threadDelay = (int)e.NewValue;
            SpeedTextBox.Text = threadDelay.ToString();
        }
    }

    private void SpeedTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UpdateSliderFromTextBox();
    }

    private void SpeedTextBox_KeyDown(object sender, KeyEventArgs e)
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
        if (int.TryParse(SpeedTextBox.Text, out int value) && value >= SpeedSlider.Minimum && value <= SpeedSlider.Maximum)
        {
            threadDelay = value;
            SpeedSlider.Value = value;
        }
        else
        {
            // Thông báo lỗi nếu giá trị không hợp lệ và reset về threadDelay
            MessageBox.Show("Please enter a valid integer within the range.");
            SpeedTextBox.Text = threadDelay.ToString();
        }
    }

    private void SpeedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Kiểm tra nếu ký tự nhập vào là số
        e.Handled = !IsTextNumeric(e.Text);
    }

    private static bool IsTextNumeric(string text)
    {
        // Kiểm tra nếu chuỗi chỉ chứa ký tự số
        return int.TryParse(text, out _);
    }

    private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
    {
        if (selectedSort != null)
        {
            selectedSort.elementCount = mylist.Count;
            selectedSort.BeginAlgorithm(mylist);
        }
    }
}