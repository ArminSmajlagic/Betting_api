using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting_api.DB_Models
{
    public class Korisnik
    {
        public int id { get; set; }
        [Required]
        public string ime_prezime { get; set; }
        [Required]
        [MinLength(4)]
        [StringLength(10)]
        public string username { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        [Required]
        public string jmbg { get; set; }
        [Required]
        public string bdate { get; set; }
        public double wallet { get; set; }
    }


}
