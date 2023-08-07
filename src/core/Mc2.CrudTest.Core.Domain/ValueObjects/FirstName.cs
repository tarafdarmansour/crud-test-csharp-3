using Mc2.CrudTest.Core.Domain.Exceptions;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record FirstName
{
    public FirstName(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 2) throw new InvalidFirstNameException(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(FirstName days)
    {
        return days.Value;
    }

    public static implicit operator FirstName(string days)
    {
        return new FirstName(days);
    }
}