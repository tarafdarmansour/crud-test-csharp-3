using Mc2.CrudTest.Core.Domain.Exceptions;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record LastName
{
    public LastName(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 2) throw new InvalidLastNameException(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(LastName days)
    {
        return days.Value;
    }

    public static implicit operator LastName(string days)
    {
        return new LastName(days);
    }
}