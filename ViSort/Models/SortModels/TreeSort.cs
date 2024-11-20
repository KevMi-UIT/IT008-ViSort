using ViSort.Draw;
using ViSort.Types;

namespace ViSort.Models.SortModels;
class TreeSort(List<int> _element, DrawRectangle _drawRectangle) : SortModel(_element, _drawRectangle), ISortModels
{
    // TODO: update info
    public static SortTypes SortType => SortTypes.Tree;
    public static string TimeComplexity => "O(n^2)";
    public static string SpaceComplexity => "O(1)";
    public static string YoutubeLink => "https://youtu.be/9I2oOAr2okY?si=GZlYC7Ab1bvFht59";
    public static string GeeksForGeeksLink => "https://www.geeksforgeeks.org/bubble-sort-algorithm/";
    //public TreeSort(Node root = null)
    //{
    //    Root = root;
    //} => NEED CONSTRUCTOR TO IMPLEMENT TREE SORT
    public class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key, Node? left = null, Node? right = null)
        {
            Key = key;
            Left = left;
            Right = right;
        }
    }

    public Node? Root { get; private set; }

    public void Insert(int Key)
    {
        Root = InsertRecord(Root, Key);
    }

    Node InsertRecord(Node Root, int Key)
    {
        if (Root == null)
        {
            Root = new Node(Key);
            return Root;
        }
        if (Key.CompareTo(Root.Key) < 0)
            Root.Left = InsertRecord(Root.Left, Key);
        else if (Key.CompareTo(Root.Key) > 0)
            Root.Right = InsertRecord(Root.Right, Key);
        return Root;
    }

    public void TreeInsert(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Insert(list[i]);
        }
    }

    private void InOrderTraversal(Node node, List<int> sortedList)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, sortedList);
            sortedList.Add(node.Key);
            InOrderTraversal(node.Right, sortedList);
        }
    }
    public override Task BeginAlgorithm()
    {
        List<int> sortedList = new List<int>();
        InOrderTraversal(Root, sortedList);
        Elements = new List<int>(sortedList);
        return Task.CompletedTask;
    }
}