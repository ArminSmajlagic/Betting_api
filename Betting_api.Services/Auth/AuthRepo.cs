using Betting_api.DB_Models;
using Dapper;
using evona_hackathon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Auth
{
    public class AuthRepo:IAuthRepo
    {
        //private readonly DB_Context db;
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly List<Token> tokens;

        //public AuthService(IConfiguration _config, DB_Context db)
        public AuthRepo(IConfiguration _config,ILogger logger)
        {
            config = _config;
            this.logger = logger;
            tokens = new List<Token>();
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

                //adding token to list of published tokens
                tokens.Add(new Token(token:token,expires: DateTime.Now.AddMinutes(15)));

                //preparing respons in json
                string jsonRes = JsonSerializer.Serialize(new { username = req.username, password = req.password, token = token, expires = DateTime.Now.AddMinutes(15).ToString() });
                
                //logging the event
                logger.Log(LogLevel.Information, req.username+" has loged in succesfully, and recived this token: " + token);
                
                return jsonRes;
            }
            //logging the event
            logger.Log(LogLevel.Error, "Username or password is invalid");

            //preparing respons in json and returning
            return JsonSerializer.Serialize("Username or password is invalid");
        }

        public async Task<string> Register(RegisterReq req)
        {
            RegisterCheckRequest check_obj = new RegisterCheckRequest() { email = req.email, username = req.username, jmbg = req.jmbg };
            var sql = "INSERT INTO Korisnik (ime_prezime,username,password,email,jmbg,bdate,wallet) VALUES (@ime_prezime,@username,@password,@email,@jmbg,@bdate,"+0.ToString()+")";
            using(var cn = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                string msg="";

                cn.Open();
                var users = await cn.QueryAsync<Korisnik>("SELECT * FROM Korisnik WHERE username=@username OR email=@email OR jmbg=@jmbg", check_obj);

                
                if (users.Count() == 0)
                {
                    await cn.ExecuteAsync(sql, req);
                    msg = "User has been added succesfully";
                }
                else
                {
                    var emails = users.Where(x => x.email == req.email).ToList();
                    var usernames = users.Where(x => x.username == req.username).ToList();
                    var jmbgs = users.Where(x=>x.jmbg==req.jmbg).ToList();

                    if (emails.Count() > 0)
                        msg += "\nUser with the given email already exists!";

                    if (usernames.Count() > 0)
                        msg += "\nUsername already exits!";

                    if (jmbgs.Count() > 0)
                        msg += "\nJMBG already exits!";
                }
                logger.Log(LogLevel.Error, msg);
                return JsonSerializer.Serialize(msg); ;
            }
        }

        public bool verifyToken(string token)
        {
            if (tokens.Any(x=>x.token==token && x.expires < DateTime.Now))
                return true;
            return false;
        }
    }
}
