using Mc2.CrudTest.Core.Domain.ValueObjects;

namespace Mc2.CrudTest.Core.Domain.Aggregates;

public class CustomerAggregate
{
    private readonly FirstName _firstName;
    private readonly LastName _lastName;
    private readonly Guid _id;

    public CustomerAggregate()
    {
        _id = Guid.NewGuid();
    }

    public CustomerAggregate(string firstName,string lastName)
    {
        _id = Guid.NewGuid();
        _firstName = firstName;
        _lastName = lastName;
    }
    
    public string GetFirstName => _firstName;
    public string GetLastName => _lastName;

    public Guid GetId()
    {
        return _id;
    }
}