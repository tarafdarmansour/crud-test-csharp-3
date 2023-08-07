using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Shouldly;

namespace Mc2.CrudTest.UnitTest;

public class CustomerAggregate_UntiTest
{
    [Fact]
    public void WhenICreateNewCustomerAggregate_ItShouldHaveValidId()
    {
        CustomerAggregate customer = new CustomerAggregate();
        Guid.TryParse(customer.GetId().ToString(), out Guid _).ShouldBe(true);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndFirstNameIsNull_ItShouldThrowException()
    {
        Action action = () => {  CustomerAggregate customer = new CustomerAggregate(null); };
        action.ShouldThrow<InvalidFirstNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetFirstName_ItShouldBeSameValue()
    {
        string myFirstName = "Mansour";
        CustomerAggregate customer = new CustomerAggregate(myFirstName);
        customer.GetFirstName.ShouldBe(myFirstName);
    }
}