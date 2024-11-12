public static class ShellSort
{
    public static void Sort<T>(List<T> list) where T : IComparable<T>
    {
        int N = list.Count;
        int i, j;
        int Increment = 3;
        T temp;
        while (Increment > 0)
        {
            for (i = 0; i < N; i++)
            {
                j = i;
                temp = list[i];
                while ((j >= Increment) && (list[j - Increment].CompareTo(temp) > 0))
                {
                    list[j] = list[j - Increment];
                    j = j - Increment;
                }
                list[j] = temp;
            }
            if (Increment / 2 != 0)
                Increment /= 2;
            else if (Increment == 1)
                Increment = 0;
            else
                Increment = 1;
        }
    }
}