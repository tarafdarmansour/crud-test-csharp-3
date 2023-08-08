using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class CustomerDuplicateEmailException : BaseException
{
    public CustomerDuplicateEmailException(string email) : base($"{email} is used by another customer.")
    {
        Email = email;
    }

    public string Email { get; }
}