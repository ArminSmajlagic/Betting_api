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
        //this model validation just needs to be added to the model, and it will be auto. called in case of error
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value.ToString().Length == 13)
                return ValidationResult.Success;

            //in case of error, the enpdoint respons will return the message bellow
            return new ValidationResult("JMBG treba imati 13 znakova!");
        }
    }
}
