using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ImageLimit : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFileCollection formFiles = value as IFormFileCollection;
            if (formFiles == null) {
                return new ValidationResult("Please Select Images");
            }
            if (formFiles.Count < 3)
            {
                return new ValidationResult("Please Select THREE Images");
            }
            else {
               return ValidationResult.Success;
            }
        }
    }
}
