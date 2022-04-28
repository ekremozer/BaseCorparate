using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BaseCorporate.Utility.Helper
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(this string email)
        {
            return !string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email);
        }
    }
}
