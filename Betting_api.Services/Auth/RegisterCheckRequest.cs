using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public class RegisterCheckRequest
    {
        public string username { get; set; }
        public string jmbg { get; set; }
        public string email { get; set; }
    }
}
