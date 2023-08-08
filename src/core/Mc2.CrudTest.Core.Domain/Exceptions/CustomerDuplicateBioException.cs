using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class CustomerDuplicateBioException : BaseException
{
    public CustomerDuplicateBioException(string firstName,string lastName,DateTimeOffset? dateOfBirth) : base($"There is another Customer with Firstname:{firstName}, Lastname:{lastName}{GetBirthDatePart(dateOfBirth)}")
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateTimeOffset? DateOfBirth { get; }

    private static string GetBirthDatePart(DateTimeOffset? dateOfBirth)
    {
        return dateOfBirth.HasValue ? $" and BirthDate: {dateOfBirth.Value.Date}" : "";
    }
}