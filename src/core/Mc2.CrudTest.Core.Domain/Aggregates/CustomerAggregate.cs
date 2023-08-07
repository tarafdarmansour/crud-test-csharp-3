using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Core.Domain.ValueObjects;
using Mc2.CrudTest.Shared.Domain;

namespace Mc2.CrudTest.Core.Domain.Aggregates;

public class CustomerAggregate : AggregateRoot
{
    private FirstName _firstName;
    private LastName _lastName;
    private DateOfBirth _dateOfBirth;
    private PhoneNumber _phoneNumber;
    private Email _email;
    private BankAccountNumber _accountNumber;

    public CustomerAggregate()
    {
        
    }
    public CustomerAggregate(string firstName, string lastName, string phoneNumber, string email, string accountNumber, DateTimeOffset? dateOfBirth = null)
    {
        RaiseEvent(new CustomerCreatedEvent
        {
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber,
            Email = email,
            AccountNumber = accountNumber,
            FirstName = firstName,
            LastName = lastName,
        });
    }

    public void Apply(CustomerCreatedEvent @event)
    {
        _id = Guid.NewGuid();
        _phoneNumber = @event.PhoneNumber;
        if (@event.DateOfBirth != null)
            _dateOfBirth = @event.DateOfBirth;
        _firstName = @event.FirstName;
        _lastName = @event.LastName;
        _email = @event.Email;
        _accountNumber = @event.AccountNumber;
    }

    #region Getters for test purpuse

    public string GetFirstName => _firstName;
    public string GetLastName => _lastName;
    public DateTimeOffset GetDateOfBirth => _dateOfBirth;
    public string GetPhoneNumber => _phoneNumber;
    public string GetEmail => _email;
    public string GetBankAccountNumber => _accountNumber;
    public Guid GetId() => _id;

    #endregion


}