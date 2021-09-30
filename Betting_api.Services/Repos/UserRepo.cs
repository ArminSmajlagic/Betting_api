using AutoMapper;
using Betting_api.DB_Models;
using Dapper;
using evona_hackathon.Data;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.BaseRepos;
using evona_hackathon.Services.IRepos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Services
{
    public class UserRepo:CrudRepo<User, Korisnik, UserSearchRequest, UserUpsertRequest>,IUserRepo
    {
        //private readonly DB_Context db;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        //public UserRepo(DB_Context _db, IMapper _mapper, IConfiguration _config, ILogger _logger) : base(_mapper, _config, _logger)
        public UserRepo(IMapper _mapper, IConfiguration _config,ILogger _logger) : base(_mapper, _config, _logger)
        {
            //db = _db;
            mapper = _mapper;
            config = _config;
            logger = _logger;
        }
      

        public override async Task<int> Update(int id, UserUpsertRequest req)
        {
            var sql = "UPDATE Korisnik SET ime_prezime = @ime_prezime, username = @username, password = @password, email = @email, jmbg = @jmbg WHERE id= "+ id.ToString();   
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql, req);
                return res;
            }
        }

        public override async Task<int> Insert(UserUpsertRequest req)
        {
            var sql = "INSERT INTO Korisnik (ime_prezime,username,password,email,jmbg,wallet) VALUES (@ime_prezime,@username,@password,@email,@jmbg,@wallet)";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql, req);
                return res;
            }
        }

        public override async Task<List<User>> GetAll(UserSearchRequest req=null)
        {
            //requires additional logic for adding AND after query arguments
            var sql = "SELECT * FROM Korisnik";
            List<string> q = new List<string>() { " ime_prezime = @ime_prezime", " username = @username", " email = @email", " jmbg = @jmbg" };
            if (req!=null)
            {
                if(req.id!=0)
                    sql += " WHERE";
                if (!string.IsNullOrEmpty(req.ime_prezime))
                    sql += q[0];
                if (!string.IsNullOrEmpty(req.username))
                    sql += q[1];
                if (!string.IsNullOrEmpty(req.email))
                    sql += q[2];
                if (!string.IsNullOrEmpty(req.jmbg))
                    sql += q[3];
            }

            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.QueryAsync<Korisnik>(sql, req);
                return mapper.Map<List<User>>(res.ToList());
            }
        }
    }
}
