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
            ThrowIfCustomerExist(cmd);
            CustomerAggregate customer = GenerateCustomerAggregate(cmd);
            await _eventSourcingHandler.SaveAsync(customer);
            return customer.GetId();
        }

        private static CustomerAggregate GenerateCustomerAggregate(NewCustomerCommand cmd)
        {
            var customer = new CustomerAggregate(cmd.FirstName, cmd.LastName, cmd.PhoneNumber, cmd.Email,
                cmd.AccountNumber, cmd.DateOfBirth);
            return customer;
        }

        private void ThrowIfCustomerExist(NewCustomerCommand cmd)
        {
            ThrowIfCustomerExistByEmail(cmd);
            ThrowIfCustomerExistByBio(cmd);
        }

        private void ThrowIfCustomerExistByBio(NewCustomerCommand cmd)
        {
            if (CustomerBioExist(cmd.FirstName, cmd.LastName, cmd.DateOfBirth))
                throw new CustomerDuplicateBioException(cmd.FirstName, cmd.LastName, cmd.DateOfBirth);
        }

        private void ThrowIfCustomerExistByEmail(NewCustomerCommand cmd)
        {
            if (EmailExist(cmd.Email))
                throw new CustomerDuplicateEmailException(cmd.Email);
        }

        private bool EmailExist(string email)
        {
            return _customerRepository.Get(c => c.Email == email).Any();
        }

        private bool CustomerBioExist(string firstName,string lastName,DateTimeOffset? dateOfBirth)
        {
             var customers = _customerRepository.Get(c => c.FirstName == firstName && c.LastName == lastName).ToList();
             if (dateOfBirth == null)
                 return customers.Any();
             return customers.Any(c => c.DateOfBirth.Value.Date == dateOfBirth.Value.Date);
        }

    }
}
