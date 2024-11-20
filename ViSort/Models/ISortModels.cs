using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViSort.Types;

namespace ViSort.Models;

public interface ISortModels
{
    static abstract SortTypes SortType { get; }
    static abstract string YoutubeLink { get; }
    static abstract string GeeksForGeeksLink { get; }
    static abstract string TimeComplexity { get; }
    static abstract string SpaceComplexity { get; }
}