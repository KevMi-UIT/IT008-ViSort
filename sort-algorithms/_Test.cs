public class ComplexType(int val) : IComparable<ComplexType>
{
    public int Val { get; set; } = val;
    public int CompareTo(ComplexType? other)
    {
        return other switch
        {
            null => throw new ArgumentException("Object is null"),
            _ => Val.CompareTo(other.Val)
        };
    }
}

public class Program
{
    public static void Main()
    {
        List<ComplexType> tests = [new(3), new(2), new(9), new(0), new(-1)];
        // QuickSort.Sort(tests, 0, tests.Count - 1);

        foreach (ComplexType test in tests)
        {
            Console.WriteLine(test.Val);
        }
    }
}
