using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidFirstNameException : BaseException
{
    public InvalidFirstNameException(string firstName) : base($"FirstFirstName value {firstName} is invalid")
    {
        FirstFirstName = firstName;
    }

    public string FirstFirstName { get; }
}