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

namespace ViSort
{
    /// <summary>
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow : Window
    {
        private int threadDelay;
        private int elementCount;
        private string selectedSortAlgorithm = "";
        private string selectedArrayGenerationMethod = "";
        private List<int> mylist;
        private Base selectedAlgorithm;
        private Dictionary<string, Func<Base>> sortingAlgorithms;
        private Dictionary<string, Func<int, List<int>>> arrayGenerationMethods;
        public DisplayWindow(int _elementCount, int _threadDelay, string _selectedSortAlgorithm, string _selectedArrayGenerationMethod)
        {
            InitializeComponent();
            elementCount = _elementCount;
            threadDelay = _threadDelay;
            selectedSortAlgorithm = _selectedSortAlgorithm;
            selectedArrayGenerationMethod = _selectedArrayGenerationMethod;
            sortingAlgorithms = new Dictionary<string, Func<Base>>()
            {
                { "BubbleSort ", () => new BubbleSort() }
            };
            arrayGenerationMethods = new Dictionary<string, Func<int, List<int>>>()
            {
                { "Normal", length => GenRandomList.GenList(length, GenRandomList.RandomGenTypes.Normal) },
                { "Sorted", length => GenRandomList.GenList(length, GenRandomList.RandomGenTypes.Sorted) },
                { "SortedReverse", length => GenRandomList.GenList(length, GenRandomList.RandomGenTypes.SortedReverse) },
                { "NearlySorted", length => GenRandomList.GenList(length, GenRandomList.RandomGenTypes.NearlySorted) },
                { "Mirror", length => GenRandomList.GenList(length, GenRandomList.RandomGenTypes.Mirror) }
            };
        }

        private List<int> GenerateSelectedList(int length)
        {
            // Find the checked RadioButton in the array generation group
            var selectedGenMethod = arrayGenerationMethods.FirstOrDefault(kvp =>
                (FindName(kvp.Key) as RadioButton)?.IsChecked == true).Value;

            // Generate the list using the selected method, or default to Normal if none selected
            return selectedGenMethod != null ? selectedGenMethod(length) : GenRandomList.GenList(length, GenRandomList.RandomGenTypes.Normal);
        }

        private Base GetSelectedAlgorithm()
        {
            // Find the checked RadioButton in the group
            var selectedAlgorithm = sortingAlgorithms.FirstOrDefault(kvp =>
                (FindName(kvp.Key) as RadioButton)?.IsChecked == true).Value;

            // Return the sorting algorithm instance or null if none selected
            return selectedAlgorithm?.Invoke();
        }

        private void SetUpFormData(string _setModifier)
        {
            SortTypes sortType = (SortTypes)Enum.Parse(typeof(SortTypes), selectedSortAlgorithm);
            //Cập nhật cách sử dụng SortTypes
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

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            mylist = GenerateSelectedList(elementCount);

            // Get the selected sorting algorithm
            Base selectedAlgorithm = GetSelectedAlgorithm();

            if (selectedAlgorithm != null)
            {
                selectedAlgorithm.elementCount = mylist.Count;
                selectedAlgorithm.BeginAlgorithm(mylist);
            }

        }
    }
}
