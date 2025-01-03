﻿using ViSort.Types;
namespace ViSort.Models;

class QuizModel(int order, string content, List<int> items)
{
    public int Order { get; set; } = order;
    public List<int> Items { get; set; } = items;
    public string Content { get; private set; } = content;
    public SortTypes? SelectedAnswer { get; set; } = null;
    public SortModel? SelectedSort { get; set; } = null;
}