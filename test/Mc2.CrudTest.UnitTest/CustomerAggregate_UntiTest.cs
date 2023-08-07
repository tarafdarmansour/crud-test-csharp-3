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
        Action action = () => {  CustomerAggregate customer = new CustomerAggregate(null,"somethings"); };
        action.ShouldThrow<InvalidFirstNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetFirstName_ItShouldBeSameValue()
    {
        string myFirstName = "Mansour";
        CustomerAggregate customer = new CustomerAggregate(myFirstName, "somethings");
        customer.GetFirstName.ShouldBe(myFirstName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndLastNameIsNull_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate("somethings",null); };
        action.ShouldThrow<InvalidLastNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetLastName_ItShouldBeSameValue()
    {
        string myLastName = "Tarafdar";
        CustomerAggregate customer = new CustomerAggregate( "somethings", myLastName);
        customer.GetLastName.ShouldBe(myLastName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsNull_ItShouldNotThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate("somethings", "somethingelse"); };
        action.ShouldNotThrow();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsInvalid_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate("somethings", "somethingelse",DateTimeOffset.Now.AddHours(1)); };
        action.ShouldThrow<InvalidDateOfBirthException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfBirth_ItShouldBeSameValue()
    {
        DateTimeOffset myBirthDate = new DateTimeOffset(1985, 5, 13,10,10,10,new TimeSpan(0));
        CustomerAggregate customer = new CustomerAggregate("somethings", "somethingelse", myBirthDate);
        customer.GetDateOfBirth.ShouldBe(myBirthDate);
    }
}