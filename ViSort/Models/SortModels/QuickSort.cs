//using ViSort.Sorts;

//public class QuickSort : SortModel
//{
//    private List<int> _List;
//    private SortCallback _Callback;
//    private int _SleepTime;

//    public QuickSort(List<int> _ListIn, SortCallback _CallbackIn, in int _SleepTimeIn)
//    {
//        _List = _ListIn;
//        _Callback = _CallbackIn;
//        _SleepTime = _SleepTimeIn;
//    }

//    private int Partition(int low, int high)
//    {
//        int pivot = _List[high];
//        int i = low - 1;
//        for (int j = low; j <= high - 1; j++)
//        {
//            if (_List[j] < pivot)
//            {
//                i++;
//                Thread.Sleep(_SleepTime);
//                swap(_List, i, j);
//                _Callback(_List);
//            }
//        }
//        Thread.Sleep(_SleepTime);
//        swap(_List, i + 1, high);
//        _Callback(_List);
//        return i + 1;
//    }

//    public void Recursive(int low, int high)
//    {
//        if (low < high)
//        {
//            int pi = Partition(low, high);
//            Recursive(low, pi - 1);
//            Recursive(pi + 1, high);
//        }
//    }

//    public async override void BeginAlgorithm()
//    {
//        Recursive(0, _List.Count - 1);
//    }
//}