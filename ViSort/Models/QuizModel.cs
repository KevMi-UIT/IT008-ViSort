using ViSort.Draw;
using ViSort.Types;
using ViSort.Utils;
namespace ViSort.Models;

class QuizModel(int order, string content, List<int> items)
{
    public int Order { get; set; } = order;
    public List<int> Items { get; set; } = items;
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; private set; } = null;
    public int Steps { get; private set; } = 0;

    public void SelectAnswer(SortTypes answer, DrawRectangle drawRectangle)
    {
        SelectedAnswer = answer;
        SortModel? sortModel = SortUtils.InstantiateSort(answer, Items, drawRectangle);
        if (sortModel != null)
        {
            Steps = sortModel.SortAndGetSteps().Result;
        }
    }

    public async Task<int> CalculateStepsAsync(SortModel sortModel)
    {
        Steps = await sortModel.SortAndGetSteps();
        return Steps;
    }
}