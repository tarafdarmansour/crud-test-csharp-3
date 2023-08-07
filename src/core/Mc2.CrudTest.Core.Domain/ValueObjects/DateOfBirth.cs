using Mc2.CrudTest.Core.Domain.Exceptions;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record DateOfBirth
{
    public DateOfBirth(DateTimeOffset value)
    {
        if (value > DateTimeOffset.Now) throw new InvalidDateOfBirthException(value);

        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static implicit operator DateTimeOffset(DateOfBirth dateOfBirth)
    {
        return dateOfBirth.Value;
    }

    public static implicit operator DateOfBirth(DateTimeOffset dateTimeOffset)
    {
        return new DateOfBirth(dateTimeOffset);
    }
}