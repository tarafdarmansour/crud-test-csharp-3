using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Shouldly;

namespace Mc2.CrudTest.UnitTest;

public class CustomerAggregate_UntiTest
{
    private readonly string someting = "something";
    private readonly string defaultPhoneNumber = "+989396135891";
    [Fact]
    public void WhenICreateNewCustomerAggregate_ItShouldHaveValidId()
    {
        CustomerAggregate customer = new CustomerAggregate();
        Guid.TryParse(customer.GetId().ToString(), out Guid _).ShouldBe(true);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndFirstNameIsNull_ItShouldThrowException()
    {
        Action action = () => {  CustomerAggregate customer = new CustomerAggregate(null, someting, defaultPhoneNumber); };
        action.ShouldThrow<InvalidFirstNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetFirstName_ItShouldBeSameValue()
    {
        string myFirstName = "Mansour";
        CustomerAggregate customer = new CustomerAggregate(myFirstName, someting, defaultPhoneNumber);
        customer.GetFirstName.ShouldBe(myFirstName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndLastNameIsNull_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate(someting,null, defaultPhoneNumber); };
        action.ShouldThrow<InvalidLastNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetLastName_ItShouldBeSameValue()
    {
        string myLastName = "Tarafdar";
        CustomerAggregate customer = new CustomerAggregate(someting, myLastName,defaultPhoneNumber);
        customer.GetLastName.ShouldBe(myLastName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsNull_ItShouldNotThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate(someting, someting, defaultPhoneNumber); };
        action.ShouldNotThrow();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsInvalid_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new CustomerAggregate(someting,someting,defaultPhoneNumber,DateTimeOffset.Now.AddHours(1)); };
        action.ShouldThrow<InvalidDateOfBirthException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfBirth_ItShouldBeSameValue()
    {
        DateTimeOffset myBirthDate = new DateTimeOffset(1985, 5, 13,10,10,10,new TimeSpan(0));
        CustomerAggregate customer = new CustomerAggregate(someting,someting,defaultPhoneNumber, myBirthDate);
        customer.GetDateOfBirth.ShouldBe(myBirthDate);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndPhoneNumberIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new CustomerAggregate(someting, someting, null);

        };
        action.ShouldThrow<InvalidPhoneNumberException>();
    }

    [Theory]
    [InlineData("1111111111")]
    [InlineData("+1111111111")]
    public void WhenICreateNewCustomerAggregate_AndPhoneNumberIsInvalid_ItShouldThrowException(string invalidNumber)
    {
        Action action = () =>
        {
            CustomerAggregate customer = new CustomerAggregate(someting, someting, invalidNumber);

        };
        action.ShouldThrow<InvalidPhoneNumberException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfPhoneNumber_ItShouldBeSameValue()
    {
        string myPhoneNumber = "00989396135891";
        CustomerAggregate customer = new CustomerAggregate(someting, someting, myPhoneNumber);
        customer.GetPhoneNumber.ShouldBe(myPhoneNumber);
    }
}