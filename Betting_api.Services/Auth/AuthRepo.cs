using Betting_api.DB_Models;
using Dapper;
using evona_hackathon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public class AuthRepo:IAuthRepo
    {
        //private readonly DB_Context db;
        private readonly IConfiguration config;

        //public AuthService(IConfiguration _config, DB_Context db)
        public AuthRepo(IConfiguration _config)
        {
            config = _config;
            //this.db = db;
        }

        public async Task<string> Login(LoginReq req)
        {

            var sql = "SELECT * FROM Korisnik WHERE username=@username AND password=@password";
            Korisnik user;
            using(var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.QueryAsync<Korisnik>(sql, req);
                var set = res.ToList();
                user = set.FirstOrDefault();
            }
                
            if (user != null)
            {
                var section = config["Token"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(section));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var opts = new JwtSecurityToken(
                   issuer: "https://localhost:44398/",
                   audience: "https://localhost:4200/",
                   claims: new List<Claim>(),
                   expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: creds
                 );
                var token = new JwtSecurityTokenHandler().WriteToken(opts);
                return token;
            }
            return "2";
        }

        public async Task<string> Register(RegisterReq req)
        {
            return "Not implemented";
        }
    }
}
