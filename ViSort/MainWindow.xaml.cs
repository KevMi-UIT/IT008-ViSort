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

namespace ViSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int threadDelay;
        private int elementCount;
        private string selectedSortAlgorithm = "";
        private string selectedArrayGenerationMethod = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        

        // Sự kiện cho RadioButton của thuật toán sắp xếp
        private void SortAlgorithm_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                selectedSortAlgorithm = radioButton.Content.ToString();
                UpdateSelectedSection();
            }
        }

        // Sự kiện cho RadioButton của phương pháp tạo mảng
        private void ArrayGenerationMethod_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                selectedArrayGenerationMethod = radioButton.Content.ToString();
                UpdateSelectedSection();
            }
        }

        // Cập nhật TextBlock với thông tin hiện tại
        private void UpdateSelectedSection()
        {
            SlectedSection.Text = $"Algorithm: {selectedSortAlgorithm}, Array: {selectedArrayGenerationMethod}";
        }
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

        private bool IsTextNumeric(string text)
        {
            // Kiểm tra nếu chuỗi chỉ chứa ký tự số
            return int.TryParse(text, out _);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            // Tạo và hiển thị cửa sổ DisplayWindow
            DisplayWindow displayWindow = new DisplayWindow(elementCount, threadDelay, selectedSortAlgorithm, selectedArrayGenerationMethod);
            displayWindow.Closed += (s, e) => this.Show();
            displayWindow.Show();

        }

    }
}