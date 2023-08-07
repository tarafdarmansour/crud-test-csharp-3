using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Shared.Validators
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return EmailValidation.EmailValidator.Validate(value);
        }
    }
}
