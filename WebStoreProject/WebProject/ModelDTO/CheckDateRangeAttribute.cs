using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.ModelDTO
{
    class CheckDateRangeAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt < DateTime.UtcNow)
            {
                return ValidationResult.Success;

            }
            return new ValidationResult(ErrorMessage ?? "Birthday can't be greater than current date");
        }
    }
}
