using Mc2.CrudTest.Core.Application.Commands.NewCustomer;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Presentation;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

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
        NewCustomerCommand newCustomerCommand = new NewCustomerCommand
        {
            DateOfBirth = DateTimeOffset.Now.AddYears(-38),
            AccountNumber = "GB03SHHZ28711587148418",
            Email = "tarafdar.mansour@gmail.com",
            FirstName = "Mansour",
            LastName = "Tarafdar",
            PhoneNumber = "00989396135891"
        };
        var bus = _factory.Services.GetRequiredService<IMediator>();
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
}