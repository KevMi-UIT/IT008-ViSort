using DnsClient.Protocol;
using MongoDB.Driver.Core.Authentication;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;
class TreeSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Tree;
    public static string TimeComplexity => "O(nlog(n))";
    public static string SpaceComplexity => "O(n)";
    public static string YoutubeLink => "https://www.youtube.com/watch?v=oRhcBqygLos";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/tree-sort/";

    public async override Task BeginAlgorithm()
    {
        List<int> currentElements = new();
        foreach (int element in Elements)
        {
            InsertAsync(element, currentElements);
            await Task.Delay(DrawRect.ThreadDelay);
        }
        List<int> sortedElements = new();
        InOrderTraversal(_root, sortedElements);
    }
    public class Node(int Key)
    {
        public int Key { get; } = Key;
        public Node? Left { get; set; } = null;
        public Node? Right { get; set; } = null;
    }

    private Node? _root = null;

    public void InsertAsync(int key, List<int> currentElements)
    {
        _root = InsertRec(_root, key);
        currentElements.Add(key);
        currentElements.Sort();
        DrawRect.DrawRectangleOnCanvas(currentElements, Colors.Black);
    }

    private Node InsertRec(Node? root, int key)
    {
        Step++;
        if (root == null)
            return new Node(key);
        if (key < root.Key)
            root.Left = InsertRec(root.Left, key);
        else if (key > root.Key)
            root.Right = InsertRec(root.Right, key);

        return root;
    }

    public void InOrderTraversal(Node? root, List<int> sortedElements)
    {
        Step++;
        if (root == null)
            return;
        InOrderTraversal(root.Left, sortedElements);
        sortedElements.Add(root.Key);
        InOrderTraversal(root.Right, sortedElements);
    }
}