namespace ViSort.Utils;

public enum GenRandomListTypes
{
    Normal,
    Sorted,
    SortedReverse,
    NearlySorted,
    Mirror,
    Adjacent,
    NearlyAdjacent
}

public static class GenRandomList
{
    public const int MIN = 10;
    public const int MAX = 999;
    public const int NEARLY_SORTED_SENSITIVITY = 10;
    private static readonly Random RAND = new();

    public static List<int> GenList(int length, GenRandomListTypes type)
    {
        return type switch
        {
            GenRandomListTypes.Normal => GenNormal(length),
            GenRandomListTypes.Sorted => GenSorted(length),
            GenRandomListTypes.SortedReverse => GenSortedReverse(length),
            GenRandomListTypes.NearlySorted => GenNearlySorted(length),
            GenRandomListTypes.Mirror => GenMirror(length),
            GenRandomListTypes.Adjacent => GenAdjacent(length),
            GenRandomListTypes.NearlyAdjacent => GenNearlyAdjacent(length),
            _ => throw new ArgumentException("Invalid list type"),
        };
    }

    public static List<int> GenNormal(int length)
    {
        return Enumerable.Range(0, length).Select(static x => RAND.Next(MIN, MAX + 1)).ToList(); //Change in Range(0, length)
    }

    public static List<int> GenSorted(int length)
    {
        List<int> list = GenNormal(length);
        list.Sort();
        return list;
    }

    public static List<int> GenSortedReverse(int length)
    {
        List<int> list = GenSorted(length);
        list.Reverse();
        return list;
    }

    public static List<int> GenNearlySorted(int length)
    {
        List<int> list = GenSorted(length);
        ShuffleList(ref list, length);
        return list;
    }

    public static List<int> GenMirror(int length)
    {
        List<int> list = new(new int[length]);
        int limit = length / 2;
        for (int i = 0; i < limit; i++)
        {
            int randomValue = RAND.Next(MIN, MAX + 1);
            list[i] = randomValue;
            list[length - i - 1] = randomValue;
        }
        if (length % 2 != 0)
        {
            list[limit] = RAND.Next(MIN, MAX + 1);
        }
        return list;
    }

    public static List<int> GenAdjacent(int length)
    {
        int i = RAND.Next(MIN, MAX - length + 1);
        return Enumerable.Range(i, length).ToList();
    }

    public static List<int> GenNearlyAdjacent(int length)
    {
        List<int> list = GenAdjacent(length);
        ShuffleList(ref list, length);
        return list;
    }

    private static void ShuffleList(ref List<int> list, int length)
    {
        int lim = MAX / NEARLY_SORTED_SENSITIVITY;
        for (int i = 0; i < lim; i++)
        {
            int a = RAND.Next(MIN, length);// Change from a = RAND.NEXT(0, LENGTH) to a = RAND.NEXT(MIN, LENGTH)
            int b = RAND.Next(MIN, length);// Change from b = RAND.NEXT(0, LENGTH) to b = RAND.NEXT(MIN, LENGTH)
            (list[b], list[a]) = (list[a], list[b]);
        }
    }
}