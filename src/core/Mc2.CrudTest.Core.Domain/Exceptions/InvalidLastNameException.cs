using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidLastNameException : BaseException
{
    public InvalidLastNameException(string lasName) : base($"LastName value {lasName} is invalid")
    {
        LastName = lasName;
    }

    public string LastName { get; }
}