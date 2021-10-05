using evona_hackathon.Models.Models_validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Models.V1_Models
{
    public class UserUpsertRequest
    {
        public string ime_prezime { get; set; }
        [MinLength(5)]
        public string username { get; set; }
        [MinLength(6)]
        [MaxLength(16)]
        public string password { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [JMBG_validation]
        public string jmbg { get; set; }
        public double wallet { get; set; }
    }
}
