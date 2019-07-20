using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class InquiryViewModel
    {
        public string Email { get; set; }

        public long? CustomerId { get; set; }

        public RequestValidationResult GetValidationResult()
        {
            var validationResult = new RequestValidationResult()
            {
                Message = "",
                IsValid = true,
            };

            if (!CustomerId.HasValue && string.IsNullOrEmpty(Email))
            {
                validationResult.Message = "No inquiry criteria";
                validationResult.IsValid = false;
            }
            else if ((!string.IsNullOrEmpty(Email) && !Email.IsValidEmail())
                && (CustomerId.HasValue && CustomerId.Value < 0))
            {
                validationResult.Message = "Invalid inquiry criteria";
                validationResult.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(Email) && !Email.IsValidEmail())
            {
                validationResult.Message = "Invalid Email";
                validationResult.IsValid = false;
            }
            else if (CustomerId.HasValue && CustomerId.Value < 0)
            {
                validationResult.Message = "Invalid Customer ID";
                validationResult.IsValid = false;
            }

            return validationResult;
        }
    }
}
