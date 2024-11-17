//public class TreeSort   
//{ 
//    public class Node   
//    { 
//        public int Key { get; set; }
//        public Node  Left { get; set; }
//        public Node  Right { get; set; }

//        public Node(int key, Node  left = null, Node  right = null)
//        {
//            Key = key; 
//            Left = left;     
//            Right = right;  
//        }
//    } 

//    public Node  Root { get; private set; }

//    public TreeSort(Node  root = null)
//    {
//        Root = root;
//    }

//    public void Insert(int Key) 
//    { 
//        Root = InsertRecord(Root, Key); 
//    } 

//    Node  InsertRecord(Node  Root, int Key) 
//    { 
//        if (Root == null) 
//        { 
//        Root = new Node (Key); 
//        return Root; 
//        }
//        if (Key.CompareTo(Root.Key) < 0) 
//        Root.Left = InsertRecord(Root.Left, Key); 
//        else if (Key.CompareTo(Root.Key) > 0) 
//        Root.Right = InsertRecord(Root.Right, Key); 
//        return Root; 
//    } 

//    public void TreeInsert(List<int> list) 
//    { 
//        for(int i = 0; i < list.Count; i++) 
//        { 
//        Insert(list[i]); 
//        } 
//    }

//private void InOrderTraversal(Node  node, List<int>  sortedList)
//    {
//        if (node != null)
//        {
//            InOrderTraversal(node.Left, sortedList);
//            sortedList.Add(node.Key);
//            InOrderTraversal(node.Right, sortedList);
//        }
//    }

//    public List<int>  GetSortedList()
//    {
//        List<int>  sortedList = new List<int> ();
//        InOrderTraversal(Root, sortedList);
//        return sortedList;
//    }
//}