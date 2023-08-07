using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Shared.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message): base(message)
        { }
    }
}
