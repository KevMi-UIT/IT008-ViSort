using static ViSort.Utils.SortUtils;

namespace ViSort.Models;

public class SortsInfoModel(SortModel sortModel)
{
    public string SortName { get; set; } = GetSortName(sortModel.SortType);
    public string GeeksForGeeksLink { get; set; } = sortModel.GeeksForGeeksLink;
    public string YoutubeLink { get; set; } = sortModel.YoutubeLink;
    public string TimeComplexity { get; set; } = sortModel.TimeComplexity;
    public string SpaceComplexity { get; set; } = sortModel.SpaceComplexity;
}