using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Shouldly;

namespace Mc2.CrudTest.UnitTest;

public class CustomerAggregateEvents_UntiTest
{
    private readonly string someting = "something";
    private readonly string defaultPhoneNumber = "+989396135891";
    private readonly string defaultEmail = "tarafdar.mansour@gmail.com";
    private readonly string defaulAccuntNumber = "GB03SHHZ28711587148418";
    [Fact]
    public void WhenICreateNewCustomerAggregate_ItShouldHaveAnEventWithCustomerCreatedEventType()
    {
        CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail, defaulAccuntNumber);
        customer.GetUncommittedChanges().ToList().ShouldContain(e => e.Type == nameof(CustomerCreatedEvent));
    }
    
}