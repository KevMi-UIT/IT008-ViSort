using System;

public static class SelectionSort {

    static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++) 
        {
            int min_idx = i;
            for (int j = i + 1; j < n; j++) 
            {
                if (list[j].CompareTo(list[min_idx]) < 0 ) 
                {
                    min_idx = j;
                }
            }
            if (min_idx != i) 
            {
                swap (list, i, min_idx);
            }
        }
    }

    static void printArray(int[] arr)
    {
        foreach(int val in arr){
            Console.Write(val + " ");
        }
        Console.WriteLine();
    }
}
