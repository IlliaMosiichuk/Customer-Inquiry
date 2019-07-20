using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Helpers
{
    public static class StringExtensionMethods
    {
        public static bool IsValidEmail(this string email)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            return emailAddressAttribute.IsValid(email);
        }
    }
}
