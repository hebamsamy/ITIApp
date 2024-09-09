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
        public int Count { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            IFormFileCollection formFiles = value as IFormFileCollection;
            if (formFiles == null) {
                return new ValidationResult("Please Select Images");
            }
            if (formFiles.Count < Count)
            {
                return new ValidationResult($"Please Select {Count} Images");
            }
            else {
               return ValidationResult.Success;
            }
        }
    }
}
