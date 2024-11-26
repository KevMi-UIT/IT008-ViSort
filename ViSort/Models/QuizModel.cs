using ViSort.Types;
namespace ViSort.Models;

class QuizModel(int order, string content)
{
    public int Order { get; set; } = order;
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; private set; } = null;

    public void SelectAnswer(SortTypes answer)
    {
        SelectedAnswer = answer;
    }
}