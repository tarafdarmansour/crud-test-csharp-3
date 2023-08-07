using Mc2.CrudTest.Shared.Exceptions;
using System;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidPhoneNumberException : BaseException
{
    public InvalidPhoneNumberException(string phoneNumber) : base($"{phoneNumber} is invalid for PhoneNumber")
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}