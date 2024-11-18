//public static class RadixSort
//{
//   public static int getMax(List<int> list)
//   {
//      int N = list.Count;
//      int M = 0;
//      for (int i = 0; i < N; i++)
//      {
//         M = Math.Max(M, list[i]);
//      }
//      return M;
//   }

//   public static void CountingSort(List<int> list, int exp)
//   {
//      int N = list.Count;
//      List<int> countList = new List<int>(new int[10]);
//      List<int> outList = new List<int>(new int[N]);

//      for (int i = 0; i < N; i++)
//      {
//         countList[(list[i] / exp) % 10]++;
//      }

//      for (int i = 1; i < 10; i++)
//      {
//         countList[i] += countList[i - 1];
//      }

//      for (int i = N - 1; i >= 0; i--)
//      {
//         outList[countList[(list[i] / exp) % 10] - 1] = list[i];
//         countList[(list[i] / exp) % 10]--;
//      }

//      for (int i = 0; i < N; i++)
//      {
//         list[i] = outList[i];
//      }
//   }

//   public static void Sort(List<int> list)
//   {
//      int m = getMax(list);
//      for (int exp = 1; m / exp > 0; exp *= 10)
//         CountingSort(list, exp);
//   }
//}