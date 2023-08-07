using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidFirstNameException : BaseException
{
    public InvalidFirstNameException(string firstName) : base($"FirstName value {firstName} is invalid")
    {
        FirstName = firstName;
    }

    public string FirstName { get; }
}