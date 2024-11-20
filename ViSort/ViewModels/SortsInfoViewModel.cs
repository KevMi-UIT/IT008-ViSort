using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViSort.ViewModels;

public partial class SortsInfoViewModel(string _sortName, string _g4gLink, string _ytLink, string _timeComplexity, string _spaceComplexity) : ObservableObject
{
    [ObservableProperty]
    public required string sortName = _sortName;

    [ObservableProperty]
    public required string g4gLink = _g4gLink;

    [ObservableProperty]
    public required string ytLink = _ytLink;

    [ObservableProperty]
    public required string timeComplexity = _timeComplexity;

    [ObservableProperty]
    public required string spaceComplexity = _spaceComplexity;
}