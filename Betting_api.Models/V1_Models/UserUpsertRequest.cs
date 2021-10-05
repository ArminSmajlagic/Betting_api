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
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        [JMBG_validation]
        public string jmbg { get; set; }
        public double wallet { get; set; }
    }
}
