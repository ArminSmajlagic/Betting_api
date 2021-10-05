using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    //custom user exception that is totaly the same as default exception
    public class UserException:Exception
    {
        //customization to user exception can be added here
        public UserException(string msg):base(msg)
        {

        }
    }
}
