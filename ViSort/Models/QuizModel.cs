using ViSort.Types;
using ViSort.Utils;
namespace ViSort.Models;

class QuizModel(int order, string content, List<int> items)
{
    public int Order { get; set; } = order;
    public List<int> Items { get; set; } = items;
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; private set; } = null;
    public SortModel? SelectedSort { get; set; } = null;
}