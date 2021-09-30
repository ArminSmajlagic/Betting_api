using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Models.V1_Models
{
    public class UserSearchRequest
    {
        public int id { get; set; }
        public string ime_prezime { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string jmbg { get; set; }
    }
}
