using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Shared.Validators
{
    public static class BankAccountNumberValidator
    {
        public static bool IsValidBankAccountNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var validator = new IbanNet.IbanValidator();
            return validator.Validate(value).IsValid;
        }
    }
}
