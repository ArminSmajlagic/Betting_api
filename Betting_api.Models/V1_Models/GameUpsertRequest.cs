using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Models.V1_Models
{
    public class GameUpsertRequest
    {
        public string team_1 { get; set; }

        public string team_2 { get; set; }

        //public byte[] team_1_img { get; set; }

        //public byte[] team_2_img { get; set; }

        public string score { get; set; }

       // public string quote { get; set; }

       // public DateTime startTime { get; set; }
    }
}
