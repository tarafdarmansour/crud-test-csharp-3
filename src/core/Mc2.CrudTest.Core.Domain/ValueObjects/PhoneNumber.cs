using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Shared.Validators;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record PhoneNumber
{
    public string Number;
    public PhoneNumber(string phoneNumber)
    {
        if(!PhoneValidator.IsValidMobileNumber(phoneNumber))
            throw new InvalidPhoneNumberException(phoneNumber);
        Number = phoneNumber;
    }
    public static implicit operator string(PhoneNumber phoneNumber)
        => phoneNumber.Number;

    public static implicit operator PhoneNumber(string phoneNumber)
        => new(phoneNumber);
}