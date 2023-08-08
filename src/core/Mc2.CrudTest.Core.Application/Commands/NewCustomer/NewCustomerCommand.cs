using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Shared.Handlers;
using MediatR;

namespace Mc2.CrudTest.Core.Application.Commands.NewCustomer
{
    public class NewCustomerCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }

    public class NewCustomerCommandHandler : IRequestHandler<NewCustomerCommand,Guid>
    {
        private readonly IEventSourcingHandler<CustomerAggregate> _eventSourcingHandler;
        private readonly ICustomerRepository _customerRepository;

        public NewCustomerCommandHandler(IEventSourcingHandler<CustomerAggregate> eventSourcingHandler, ICustomerRepository customerRepository)
        {
            _eventSourcingHandler = eventSourcingHandler;
            _customerRepository = customerRepository;
        }
        
        public async Task<Guid> Handle(NewCustomerCommand cmd, CancellationToken cancellationToken)
        {
            if (EmailExist(cmd.Email))
                throw new CustomerDuplicateEmailException(cmd.Email);

            var customer = new CustomerAggregate(cmd.FirstName, cmd.LastName, cmd.PhoneNumber, cmd.Email,
                cmd.AccountNumber, cmd.DateOfBirth);
            await _eventSourcingHandler.SaveAsync(customer);
            return customer.GetId();
        }

        private bool EmailExist(string email)
        {
            return _customerRepository.Get(c => c.Email == email).Any();
        }
    }
}
