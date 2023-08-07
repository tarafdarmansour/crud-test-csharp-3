using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Shouldly;

namespace Mc2.CrudTest.UnitTest;

public class CustomerAggregate_UntiTest
{
    private readonly string someting = "something";
    private readonly string defaultPhoneNumber = "+989396135891";
    private readonly string defaultEmail = "tarafdar.mansour@gmail.com";
    private readonly string defaulAccuntNumber = "GB03SHHZ28711587148418";
    [Fact]
    public void WhenICreateNewCustomerAggregate_ItShouldHaveValidId()
    {
        CustomerAggregate customer = new();
        Guid.TryParse(customer.GetId().ToString(), out Guid _).ShouldBe(true);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndFirstNameIsNull_ItShouldThrowException()
    {
        Action action = () => {  CustomerAggregate customer = new(null, someting, defaultPhoneNumber, defaultEmail, defaulAccuntNumber); };
        action.ShouldThrow<InvalidFirstNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetFirstName_ItShouldBeSameValue()
    {
        string myFirstName = "Mansour";
        CustomerAggregate customer = new(myFirstName, someting, defaultPhoneNumber, defaultEmail, defaulAccuntNumber);
        customer.GetFirstName.ShouldBe(myFirstName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndLastNameIsNull_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting,null, defaultPhoneNumber,defaultEmail,defaulAccuntNumber); };
        action.ShouldThrow<InvalidLastNameException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetLastName_ItShouldBeSameValue()
    {
        string myLastName = "Tarafdar";
        CustomerAggregate customer = new(someting, myLastName,defaultPhoneNumber, defaultEmail, defaulAccuntNumber);
        customer.GetLastName.ShouldBe(myLastName);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsNull_ItShouldNotThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail,defaulAccuntNumber); };
        action.ShouldNotThrow();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndDateOfBirthIsInvalid_ItShouldThrowException()
    {
        Action action = () => { CustomerAggregate customer = new(someting,someting,defaultPhoneNumber,defaultEmail,defaulAccuntNumber,DateTimeOffset.Now.AddHours(1)); };
        action.ShouldThrow<InvalidDateOfBirthException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfBirth_ItShouldBeSameValue()
    {
        DateTimeOffset myBirthDate = new(1985, 5, 13,10,10,10,new TimeSpan(0));
        CustomerAggregate customer = new(someting,someting,defaultPhoneNumber,defaultEmail, defaulAccuntNumber, myBirthDate);
        customer.GetDateOfBirth.ShouldBe(myBirthDate);
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndPhoneNumberIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, null,defaultEmail, defaulAccuntNumber);

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
            CustomerAggregate customer = new(someting, someting, invalidNumber, defaultEmail, defaulAccuntNumber);

        };
        action.ShouldThrow<InvalidPhoneNumberException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfPhoneNumber_ItShouldBeSameValue()
    {
        string myPhoneNumber = "00989396135891";
        CustomerAggregate customer = new(someting, someting, myPhoneNumber,defaultEmail, defaulAccuntNumber);
        customer.GetPhoneNumber.ShouldBe(myPhoneNumber);
    }


    public void WhenICreateNewCustomerAggregate_AndEmailIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, null, defaultEmail, defaulAccuntNumber);

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
            CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, invalidEmail, defaulAccuntNumber);

        };
        action.ShouldThrow<InvalidEmailException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfEmail_ItShouldBeSameValue()
    {
        string myEmail = "tarafdar.mansour@gmail.com";
        CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, myEmail, defaulAccuntNumber);
        customer.GetEmail.ShouldBe(myEmail);
    }

    public void WhenICreateNewCustomerAggregate_AndBankAccountNumberIsNull_ItShouldThrowException()
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail, null);

        };
        action.ShouldThrow<InvalidIbanException>();
    }

    [Theory]
    [InlineData("123131313")]
    [InlineData("IR22222222222222222222222222")]
    [InlineData("IR123")]
    public void WhenICreateNewCustomerAggregate_AndBankAccuntNumberIsInvalid_ItShouldThrowException(string invalidAccountNumber)
    {
        Action action = () =>
        {
            CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail, invalidAccountNumber);

        };
        action.ShouldThrow<InvalidIbanException>();
    }

    [Fact]
    public void WhenICreateNewCustomerAggregate_AndSetDateOfBankAccountNumber_ItShouldBeSameValue()
    {
        string myIban = "GB03SHHZ28711587148418";
        CustomerAggregate customer = new(someting, someting, defaultPhoneNumber, defaultEmail, myIban);
        customer.GetBankAccountNumber.ShouldBe(myIban);
    }
}