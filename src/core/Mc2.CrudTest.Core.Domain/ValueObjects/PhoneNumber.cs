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
        Number = RemovePrefixFromMobileNumber(phoneNumber);
    }
    public static implicit operator string(PhoneNumber phoneNumber)
        => phoneNumber.Number;

    public static implicit operator PhoneNumber(string phoneNumber)
        => new(phoneNumber);

    private string RemovePrefixFromMobileNumber(string phoneNumber)
    {
        if (phoneNumber.StartsWith("00"))
            return phoneNumber.Substring(2);
        if(phoneNumber.StartsWith("0"))
            return phoneNumber.Substring(1);
        if (phoneNumber.StartsWith("+"))
            return phoneNumber.Substring(1);
        
        return phoneNumber;
    } 
}