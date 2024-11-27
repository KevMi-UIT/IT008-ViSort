namespace ViSort.Exceptions;

public class SortExceptions
{
    public class SortNotImplemented(string message = "") : NotImplementedException(message)
    {
    }

    public class SortUdefined(string message = "") : NotImplementedException(message)
    {
    }
}