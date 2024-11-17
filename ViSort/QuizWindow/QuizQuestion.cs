using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViSort.Sorts;

namespace ViSort.QuizWindow;

public class QuizQuestion(string content)
{
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; private set; } = null;

    public void SelectAnswer(SortTypes answer)
    {
        SelectedAnswer = answer;
    }
}