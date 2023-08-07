using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Shared.Validators
{
    public static class PhoneValidator
    {
        public static bool IsValidMobileNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (!value.StartsWith("0") && !value.StartsWith("00") && !value.StartsWith("+")) return false;

            if (value.StartsWith("00"))
                value = $"+{value.Substring(2)}";
            if (value.StartsWith("0"))
                value = $"+{value.Substring(1)}";

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneUtil.Parse(value, "");

            return phoneUtil.IsValidNumber(phoneNumber) && phoneUtil.GetNumberType(phoneNumber) == PhoneNumberType.MOBILE;
        }
    }
}
