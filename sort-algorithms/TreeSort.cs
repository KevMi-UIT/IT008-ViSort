public class TreeSort<T> where T : IComparable<T>
{
    public class Node<T> where T : IComparable<T>
    {
        public T Key { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T key, Node<T> left = null, Node<T> right = null)
        {
            Key = key;
            Left = left;
            Right = right;
        }
    }

    public Node<T> Root { get; private set; }

    public TreeSort(Node<T> root = null)
    {
        Root = root;
    }

    public void Insert(T Key)
    {
        Root = InsertRecord(Root, Key);
    }

    Node<T> InsertRecord(Node<T> Root, T Key)
    {
        if (Root == null)
        {
            Root = new Node<T>(Key);
            return Root;
        }
        if (Key.CompareTo(Root.Key) < 0)
            Root.Left = InsertRecord(Root.Left, Key);
        else if (Key.CompareTo(Root.Key) > 0)
            Root.Right = InsertRecord(Root.Right, Key);
        return Root;
    }

    public void TreeInsert(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Insert(list[i]);
        }
    }

    private void InOrderTraversal(Node<T> node, List<T> sortedList)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, sortedList);
            sortedList.Add(node.Key);
            InOrderTraversal(node.Right, sortedList);
        }
    }

    public List<T> GetSortedList()
    {
        List<T> sortedList = new List<T>();
        InOrderTraversal(Root, sortedList);
        return sortedList;
    }
}
