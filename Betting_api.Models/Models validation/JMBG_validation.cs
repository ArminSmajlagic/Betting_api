using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Models.Models_validation
{
    public class JMBG_validation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value.ToString().Length == 13)
                return ValidationResult.Success;

            return new ValidationResult("JMBG treba imati 13 znakova!");
        }
    }
}
