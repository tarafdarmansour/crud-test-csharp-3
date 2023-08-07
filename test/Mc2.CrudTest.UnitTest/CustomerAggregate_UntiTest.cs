using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Shouldly;

namespace Mc2.CrudTest.UnitTest;

public class CustomerAggregate_UntiTest
{
    private readonly string someting = "something";
    private readonly string defaultPhoneNumber = "+989396135891";
    private readonly string defaultEmail = "tarafdar.mansour@gmail.com";
    [Fact]
    public void WhenICreateNewCustomerAggregate_ItShouldHaveValidId()
    {
        CustomerAggregate customer = new();
        Guid.TryParse(customer.GetId().ToString(), out Guid _).ShouldBe(true);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndFirstNameIsNull_ItShouldThrowException()
    {
        Action action = () => {  CustomerAggregate customer = new(null, someting, defaultPhoneNumber, defaultEmail); };
        action.ShouldThrow<InvalidFirstNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetFirstName_ItShouldBeSameValue()
    {
        string myFirstName = "Mansour";
        CustomerAggregate customer = new(myFirstName, someting, defaultPhoneNumber, defaultEmail);
        customer.GetFirstName.ShouldBe(myFirstName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndLastNameIsNull_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting,null, defaultPhoneNumber,defaultEmail); };
        action.ShouldThrow<InvalidLastNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetLastName_ItShouldBeSameValue()
    {
        string myLastName = "Tarafdar";
        CustomerAggregate customer = new(someting, myLastName,defaultPhoneNumber, defaultEmail);
        customer.GetLastName.ShouldBe(myLastName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsNull_ItShouldNotThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail); };
        action.ShouldNotThrow();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsInvalid_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting,someting,defaultPhoneNumber,defaultEmail,DateTimeOffset.Now.AddHours(1)); };
        action.ShouldThrow<InvalidDateOfBirthException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfBirth_ItShouldBeSameValue()
    {
        DateTimeOffset myBirthDate = new(1985, 5, 13,10,10,10,new TimeSpan(0));
        CustomerAggregate customer = new(someting,someting,defaultPhoneNumber,defaultEmail, myBirthDate);
        customer.GetDateOfBirth.ShouldBe(myBirthDate);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndPhoneNumberIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, null,defaultEmail);

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
            CustomerAggregate customer = new(someting, someting, invalidNumber, defaultEmail);

        };
        action.ShouldThrow<InvalidPhoneNumberException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfPhoneNumber_ItShouldBeSameValue()
    {
        string myPhoneNumber = "00989396135891";
        CustomerAggregate customer = new(someting, someting, myPhoneNumber,defaultEmail);
        customer.GetPhoneNumber.ShouldBe(myPhoneNumber);
    }


    public void WhenICreateNewCustomerAggregate_AndEmailIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, null, defaultEmail);

        };
        action.ShouldThrow<InvalidEmailException>();
    }

    [Theory]
    [InlineData("Hithere")]
    [InlineData("google.com")]
    [InlineData("ads.google.com")]
    public void WhenICreateNewCustomerAggregate_AndEmailIsInvalid_ItShouldThrowException(string invalidEmail)
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, invalidEmail);

        };
        action.ShouldThrow<InvalidEmailException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfEmail_ItShouldBeSameValue()
    {
        string myEmail = "tarafdar.mansour@gmail.com";
        CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, myEmail);
        customer.GetEmail.ShouldBe(myEmail);
    }
}