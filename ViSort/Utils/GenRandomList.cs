namespace ViSort.Utils;

public enum GenRandomListTypes
{
    Normal,
    Sorted,
    SortedReverse,
    NearlySorted,
    Mirror
}

public static class GenRandomList
{
    public const int MIN = 0;
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
            _ => throw new ArgumentException("Invalid list type"),
        };
    }

    static List<int> GenNormal(int length)
    {
        return Enumerable.Range(0, length).Select(x => RAND.Next(MIN, MAX + 1)).ToList();
    }

    static List<int> GenSorted(int length)
    {
        int i = RAND.Next(MIN, MAX - length);
        return Enumerable.Range(i, i + length + 1).ToList();
    }

    static List<int> GenSortedReverse(int length)
    {
        List<int> list = GenSorted(length);
        list.Reverse();
        return list;
    }

    static List<int> GenNearlySorted(int length)
    {
        List<int> list = GenSorted(length);
        int lim = MAX / NEARLY_SORTED_SENSITIVITY;
        for (int i = 0; i < lim; i++)
        {
            int a = RAND.Next(0, length);
            int b = RAND.Next(0, length);
            (list[b], list[a]) = (list[a], list[b]);
        }
        return list;
    }

    static List<int> GenMirror(int length)
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
}