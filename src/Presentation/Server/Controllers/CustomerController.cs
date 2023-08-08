using Mc2.CrudTest.Core.Application.Commands.NewCustomer;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Presentation.Server.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _bus;

    public CustomerController(ICustomerRepository customerRepository,IMediator bus)
    {
        _customerRepository = customerRepository;
        _bus = bus;
    }

    [HttpGet]
    public IEnumerable<CustomerDto> Get()
    {
        return _customerRepository.GetAll()
            .Select(c => new CustomerDto
            {
                AccountNumber = c.AccountNumber,
                PhoneNumber = c.PhoneNumber,
                DateOfBirth = c.DateOfBirth == null ? "" : c.DateOfBirth.Value.Date.ToString("MM/dd/yyyy"),
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
            });
    }

    [HttpPost]
    public async Task Post(NewCustomerCommand command)
    {
        await _bus.Send(command);
    }
}