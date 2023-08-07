using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Shared.Validators;

namespace Mc2.CrudTest.Core.Domain.ValueObjects;

public record BankAccountNumber
{
    public string Number;
    public BankAccountNumber(string bankAccountNumber)
    {
        if(!BankAccountNumberValidator.IsValidBankAccountNumber(bankAccountNumber))
            throw new InvalidIbanException(bankAccountNumber);
        Number = bankAccountNumber;
    }
    public static implicit operator string(BankAccountNumber email)
        => email.Number;

    public static implicit operator BankAccountNumber(string email)
        => new(email);
}