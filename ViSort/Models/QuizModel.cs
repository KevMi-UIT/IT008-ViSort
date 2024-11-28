using System.Windows.Controls;
using ViSort.Types;
namespace ViSort.Models;

class QuizModel(int order, string content)
{
    public int Order { get; set; } = order;
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; private set; } = null;
    public int Steps { get; private set; } = 0;

    public void SelectAnswer(SortTypes answer, Canvas canvas) // Add canvas parameter
    {
        SelectedAnswer = answer;
        // Chuyển đổi nội dung thành danh sách các số nguyên
        List<int> elements = Content.Split(',').Select(int.Parse).ToList();
        // Tạo instance của thuật toán dựa trên SortTypes
        SortModel? sortModel = answer.CreateSortInstance(elements, canvas); // Pass canvas parameter
        // Nếu tạo thành công, tính số bước
        if (sortModel != null)
        {
            Steps = sortModel.SortAndGetSteps().Result; // Tính số bước đồng bộ
        }
    }
    public async Task<int> CalculateStepsAsync(SortModel sortModel)
    {
        Steps = await sortModel.SortAndGetSteps();
        return Steps;
    }
}