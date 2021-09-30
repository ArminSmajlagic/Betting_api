using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public class RegisterReq
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string jmbg { get; set; }
        public string ime_prezime { get; set; }
        public DateTime godine { get; set; }
    }
}
