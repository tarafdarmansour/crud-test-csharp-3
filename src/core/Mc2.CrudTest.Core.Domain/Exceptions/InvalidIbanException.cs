using Mc2.CrudTest.Shared.Exceptions;
using System;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidIbanException : BaseException
{
    public InvalidIbanException(string bankAccountNumber) : base($"{bankAccountNumber} is invalid for BankAccountNumber")
    {
        BankAccountNumber = bankAccountNumber;
    }

    public string BankAccountNumber { get; }
}