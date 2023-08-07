using Mc2.CrudTest.Core.Domain.ValueObjects;

namespace Mc2.CrudTest.Core.Domain.Aggregates;

public class CustomerAggregate
{
    private readonly FirstName _firstName;
    private readonly LastName _lastName;
    private readonly DateOfBirth _dateOfBirth;
    private readonly PhoneNumber _phoneNumber;
    private readonly Email _email;
    private readonly BankAccountNumber _accountNumber;
    private readonly Guid _id;

    public CustomerAggregate()
    {
        _id = Guid.NewGuid();
    }

    public CustomerAggregate(string firstName,string lastName,string phoneNumber,string email,string accountNumber, DateTimeOffset? dateOfBirth = null)
    {
        _id = Guid.NewGuid();
        _firstName = firstName;
        _lastName = lastName;
        _phoneNumber = phoneNumber;
        _email = email;
        _accountNumber = accountNumber;
        if (dateOfBirth != null)
        {
            _dateOfBirth = dateOfBirth.Value;
        }
    }
    
    public string GetFirstName => _firstName;
    public string GetLastName => _lastName;
    public DateTimeOffset GetDateOfBirth => _dateOfBirth;
    public string GetPhoneNumber => _phoneNumber;
    public string GetEmail => _email;
    public string GetBankAccountNumber => _accountNumber;

    public Guid GetId()
    {
        return _id;
    }
}