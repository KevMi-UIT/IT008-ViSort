using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViSort.Exceptions;

public class GenRandomListExceptions
{
    public class GenRandomListNotImplemented(string message = "") : NotImplementedException(message)
    {
    }
}