//public static class CountingSort
//{
//    public static List<int> Sort(List<int> list)
//    {
//        int N = list.Count;
//        int M = 0;
//        for (int i = 0; i < N; i++)
//        {
//            M = Math.Max(M, list[i]);
//        }
//        List<int> countList = new(M + 1);
//        for (int i = 0; i < N; i++)
//        {
//            countList[list[i]]++;
//        }
//        for (int i = 1; i <= M; i++)
//        {
//            countList[i] += countList[i - 1];
//        }
//        List<int> outList = new(N);
//        for (int i = N - 1; i >= 0; i--)
//        {
//            outList[countList[list[i]] - 1] = list[i];
//            countList[list[i]]--;
//        }
//        return outList;
//    }
//}