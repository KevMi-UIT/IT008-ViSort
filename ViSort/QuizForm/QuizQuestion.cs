using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViSort.Sorts;

namespace ViSort.QuizForm;

internal class QuizQuestion(string content)
{
    internal string Content { get; private set; } = content;
    internal SortTypes? SelectedAnswer { get; private set; } = null;

    internal void SelectAnswer(SortTypes answer)
    {
        SelectedAnswer = answer;
    }
}