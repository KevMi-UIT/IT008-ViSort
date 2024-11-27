namespace ViSort.Exceptions;

public class GenRandomListExceptions
{
    public class GenRandomListNotImplemented(string message = "") : NotImplementedException(message)
    {
    }
}