using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ViSort.Models;
using static ViSort.Utils.Utils;

namespace ViSort.ViewModels;

public partial class SortsInfoViewModel() : ObservableObject
{
    [ObservableProperty]
    public required SortsInfoModel model;

    public IEnumerable<string> AutoSuggestBoxItems => [
        "Bubble Sort",
        "Bucket Sort",
        "Counting Sort",
        "Heap Sort",
        "Insertion Sort",
        "Merge Sort",
        "Quick Sort",
        "Radix Sort",
        "Selection Sort",
        "Shell Sort",
        "Tim Sort",
        "Tree Sort"
    ];

    public ICommand OpenGeeksForGeeksLinkCommand => new RelayCommand(() => OpenLink(Model.GeeksForGeeksLink));
    public ICommand OpenYoutubeLinkCommand => new RelayCommand(() => OpenLink(Model.YoutubeLink));
}