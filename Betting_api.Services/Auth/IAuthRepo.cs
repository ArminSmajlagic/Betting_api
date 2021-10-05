using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public interface IAuthRepo
    {
        Task<string> Login(LoginReq req);
        Task<string> Register(RegisterReq req);
        bool verifyToken(string token);
    }
}
