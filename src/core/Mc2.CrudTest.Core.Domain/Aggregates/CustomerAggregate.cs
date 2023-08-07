using Mc2.CrudTest.Core.Domain.ValueObjects;

namespace Mc2.CrudTest.Core.Domain.Aggregates;

public class CustomerAggregate
{
    private readonly FirstName _firstName;
    private readonly LastName _lastName;
    private readonly DateOfBirth _dateOfBirth;
    private readonly PhoneNumber _phoneNumber;
    private readonly Guid _id;

    public CustomerAggregate()
    {
        _id = Guid.NewGuid();
    }

    public CustomerAggregate(string firstName,string lastName,string phoneNumber,DateTimeOffset? dateOfBirth = null)
    {
        _id = Guid.NewGuid();
        _firstName = firstName;
        _lastName = lastName;
        _phoneNumber = phoneNumber;
        if (dateOfBirth != null)
        {
            _dateOfBirth = dateOfBirth.Value;
        }
    }
    
    public string GetFirstName => _firstName;
    public string GetLastName => _lastName;
    public DateTimeOffset GetDateOfBirth => _dateOfBirth;
    public string GetPhoneNumber => _phoneNumber;

    public Guid GetId()
    {
        return _id;
    }
}