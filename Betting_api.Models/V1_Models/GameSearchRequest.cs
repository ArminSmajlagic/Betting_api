using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Models.V1_Models
{
    public class GameSearchRequest
    {
        public int id { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string team_1 { get; set; }
        public string team_2 { get; set; }
    }
}
