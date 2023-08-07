using Mc2.CrudTest.Core.Domain.ValueObjects;
using Mc2.CrudTest.Shared.Events;

namespace Mc2.CrudTest.Core.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent() : base(nameof(CustomerCreatedEvent))
    { }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
}