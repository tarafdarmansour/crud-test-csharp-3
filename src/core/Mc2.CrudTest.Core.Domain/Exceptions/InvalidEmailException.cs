using Mc2.CrudTest.Shared.Exceptions;
using System;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class InvalidEmailException : BaseException
{
    public InvalidEmailException(string email) : base($"{email} is invalid for Email")
    {
        Email = email;
    }

    public string Email { get; }
}