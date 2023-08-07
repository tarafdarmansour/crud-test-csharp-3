using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Shared.Validators;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record Email
{
    public string Number;
    public Email(string email)
    {
        if(!EmailValidator.IsValidEmail(email))
            throw new InvalidEmailException(email);
        Number = email;
    }
    public static implicit operator string(Email email)
        => email.Number;

    public static implicit operator Email(string email)
        => new(email);
}