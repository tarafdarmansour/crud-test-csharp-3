using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Core.Domain.Aggregates;
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

        public NewCustomerCommandHandler(IEventSourcingHandler<CustomerAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }
        
        public async Task<Guid> Handle(NewCustomerCommand cmd, CancellationToken cancellationToken)
        {
            var customer = new CustomerAggregate(cmd.FirstName, cmd.LastName, cmd.PhoneNumber, cmd.Email,
                cmd.AccountNumber, cmd.DateOfBirth);
            await _eventSourcingHandler.SaveAsync(customer);
            return customer.GetId();
        }
    }
}
