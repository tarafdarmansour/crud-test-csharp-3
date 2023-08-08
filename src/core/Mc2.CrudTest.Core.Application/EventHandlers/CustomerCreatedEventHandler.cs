using Mc2.CrudTest.Core.Domain.Entity;
using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Core.Domain.Repositories;
using MediatR;

namespace Mc2.CrudTest.Core.Application.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreatedEventHandler( ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _customerRepository.CreateAsync(new CustomerEntity()
        {
            FirstName = notification.FirstName,
            LastName = notification.LastName,
            PhoneNumber = notification.PhoneNumber,
            Email = notification.Email,
            AccountNumber = notification.AccountNumber,
            DateOfBirth = notification.DateOfBirth,
            Id = notification.Id
        });
    }
}