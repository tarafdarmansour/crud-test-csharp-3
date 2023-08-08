using FluentAssertions;
using Mc2.CrudTest.Core.Application.Commands.NewCustomer;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Presentation;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Reflection;
using Bogus;

namespace Mc2.CrudTest.IntegrationTest;

public class NewCustomerCommand_IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public NewCustomerCommand_IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void GivenNewCustomerCommand_AndDataIsValid_WhenIExecuteCommand_AndGetCustomerById_ItShouldBeSame()
    {
        Environment.SetEnvironmentVariable("RABBIT_EXCHANGE", "CustomerEvents");
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Guid customerId = await bus.Send(newCustomerCommand);

        IEventSourcingHandler<CustomerAggregate> eventSourcing =
            _factory.Services.GetRequiredService<IEventSourcingHandler<CustomerAggregate>>();
        CustomerAggregate customerInEventSource = await eventSourcing.GetByIdAsync(customerId);

        customerInEventSource.ShouldNotBeNull();
        customerInEventSource.GetDateOfBirth.ShouldBeEquivalentTo(newCustomerCommand.DateOfBirth);
        customerInEventSource.GetPhoneNumber.ShouldBeEquivalentTo(newCustomerCommand.PhoneNumber);
        customerInEventSource.GetBankAccountNumber.ShouldBeEquivalentTo(newCustomerCommand.AccountNumber);
        customerInEventSource.GetEmail.ShouldBeEquivalentTo(newCustomerCommand.Email);
        customerInEventSource.GetFirstName.ShouldBeEquivalentTo(newCustomerCommand.FirstName);
        customerInEventSource.GetLastName.ShouldBeEquivalentTo(newCustomerCommand.LastName);
    }

    [Fact]
    public async void GivenNewCustomerCommand_AndDataIsValid_WhenIAddCustomerWithDuplicateEmail_ItShouldThrowException()
    {
        Environment.SetEnvironmentVariable("RABBIT_EXCHANGE", "CustomerEvents");
        var email = GetFakeEmail();
        NewCustomerCommand newCustomerCommand1 = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = email,
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        await bus.Send(newCustomerCommand1);
        NewCustomerCommand newCustomerCommand2 = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = email,
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        Action action = () => bus.Send(newCustomerCommand2).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<CustomerDuplicateEmailException>();
    }

    [Fact]
    public async void GivenNewCustomerCommand_AndDataIsValid_WhenIAddCustomerWithDuplicateBio_ItShouldThrowException()
    {
        Environment.SetEnvironmentVariable("RABBIT_EXCHANGE", "CustomerEvents");
        var firstName = GetFakeFirstName();
        var lastName = GetFakeLastName();
        var dateOfBirth = GetFakeDateOfBirth();
        NewCustomerCommand newCustomerCommand1 = new()
        {
            DateOfBirth = dateOfBirth,
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        await bus.Send(newCustomerCommand1);
        NewCustomerCommand newCustomerCommand2 = new()
        {
            DateOfBirth = dateOfBirth,
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "00989396135891"
        };
        Action action = () => bus.Send(newCustomerCommand2).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<CustomerDuplicateEmailException>();
    }


    [Fact]
    public void GivenNewCustomerCommand_AndDateOfBirthIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = DateTimeOffset.Now.AddYears(1),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidDateOfBirthException>();
    }

    [Fact]
    public void GivenNewCustomerCommand_AndAccountNumberIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB11111111111111111",
            Email = GetFakeEmail(),
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidIbanException>();
    }

    [Fact]
    public void GivenNewCustomerCommand_AndEmailIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = "tarafdar.mansour",
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidEmailException>();
    }

    [Fact]
    public void GivenNewCustomerCommand_AndFirstNameIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = "",
            LastName = GetFakeLastName(),
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidFirstNameException>();
    }

    [Fact]
    public void GivenNewCustomerCommand_AndLastNameIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = GetFakeFirstName(),
            LastName = "",
            PhoneNumber = "00989396135891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidLastNameException>();
    }

    [Fact]
    public void GivenNewCustomerCommand_AndPhoneNumberIsInvalid_WhenIExecuteCommand_ItShouldThrowException()
    {
        NewCustomerCommand newCustomerCommand = new()
        {
            DateOfBirth = GetFakeDateOfBirth(),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = GetFakeEmail(),
            FirstName = GetFakeFirstName(),
            LastName = GetFakeLastName(),
            PhoneNumber = "00985891"
        };
        IMediator bus = _factory.Services.GetRequiredService<IMediator>();
        Action action = () => bus.Send(newCustomerCommand).GetAwaiter().GetResult();
        action.Should()
            .Throw<TargetInvocationException>()
            .WithInnerException<InvalidPhoneNumberException>();
    }

    private string GetFakeEmail()
    {
        Faker faker = new Faker();
        return faker.Internet.Email();
    }
    private string GetFakeFirstName()
    {
        Faker faker = new Faker();
        return faker.Person.FirstName;
    }
    private string GetFakeLastName()
    {
        Faker faker = new Faker();
        return faker.Person.LastName;
    }
    private DateTime GetFakeDateOfBirth()
    {
        Faker faker = new Faker();
        return faker.Person.DateOfBirth;
    }
}