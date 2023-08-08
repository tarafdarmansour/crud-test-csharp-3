using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using FluentValidation;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Validators;
using MediatR;

namespace Mc2.CrudTest.Core.Application.Commands.NewCustomer
{
    public class AddMagazineCommandValidator : AbstractValidator<NewCustomerCommand>
    {
        public AddMagazineCommandValidator()
        {
            RuleFor(c => c.Email)
                .Must(EmailValidator.IsValidEmail)
                .WithMessage("You must provide a valid email address");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(1)
                .WithName("FirstName");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(1)
                .WithName("LastName");

            //RuleFor(x => x.DateOfBirth)
            //    .Must(dob => dob != null && dob.Value.Date < DateTime.Now)
            //    .WithMessage("BirthDate value is not valid");

            RuleFor(x => x.PhoneNumber)
                .Must(PhoneValidator.IsValidMobileNumber)
                .WithMessage("PhoneNumber value is not valid");

            RuleFor(x => x.AccountNumber)
                .Must(BankAccountNumberValidator.IsValidBankAccountNumber)
                .WithMessage("AccountNumber value is not valid");
        }
    }
}
