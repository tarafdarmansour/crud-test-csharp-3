using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidDateOfBirthException : BaseException
{
    public InvalidDateOfBirthException(DateTimeOffset dateTime) : base($"{dateTime} is invalid for DateOfBirth")
    {
        dateTime = dateTime;
    }

    public DateTimeOffset dateTime { get; }
}