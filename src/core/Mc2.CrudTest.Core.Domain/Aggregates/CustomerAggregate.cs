using Mc2.CrudTest.Core.Domain.ValueObjects;

namespace Mc2.CrudTest.Core.Domain.Aggregates;

public class CustomerAggregate
{
    private readonly FirstName _firstName;
    private readonly Guid _id;

    public CustomerAggregate()
    {
        _id = Guid.NewGuid();
    }

    public CustomerAggregate(string firstName)
    {
        _id = Guid.NewGuid();
        _firstName = firstName;
    }

    public string GetFirstName => _firstName;

    public Guid GetId()
    {
        return _id;
    }
}