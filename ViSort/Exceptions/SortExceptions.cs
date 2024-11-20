using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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