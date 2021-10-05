using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public class Token
    {
        public string token { get; set; }
        public DateTime expires { get; set; }
        public Token(string token, DateTime expires)
        {
            this.token = token;
            this.expires = expires;
        }
    }
}
