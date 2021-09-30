using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting_api.DB_Models
{
    public class Korisnik
    {
        public int id { get; set; }
        public string ime_prezime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        public string jmbg { get; set; }
        public double wallet { get; set; }
    }
}
