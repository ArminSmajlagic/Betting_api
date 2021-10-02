using AutoMapper;
using Betting_api.DB_Models;
using Dapper;
using evona_hackathon.Data;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.BaseRepos;
using evona_hackathon.Services.IBaseRepos;
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
    public class GameRepo : CrudRepo<Game, Igra, GameSearchRequest, GameUpsertRequest>, IGameRepo
    {
        //private readonly DB_Context db; SQL + EF Context
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        //public GameRepo(DB_Context _db, IMapper _mapper, IConfiguration _config, ILogger _logger) : base(_db, _mapper, _config, _logger)
        public GameRepo(IMapper _mapper, IConfiguration _config, ILogger _logger) : base(_mapper, _config, _logger)
        {
            //db = _db;
            mapper = _mapper;
            config = _config;
            logger = _logger;
        }

        //conditional update based on req argument fields
        public override async Task<int> Update(int id, GameUpsertRequest req)
        {
            var sql = "UPDATE Igra ";
            if (req != null)
            {
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " SET team_1=@team_1";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,team_2=@team_2";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,team_1_img=@team_1_img";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,team_2_img=@team_2_img";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,score=@score";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,quote=@quote";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,currentTime=@currentTime";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " ,startTime=@startTime";
            }
            else
            {
                return 0;
            }
            sql += " WHERE id=@id";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql, req);
                return res;
            }
        }

        public override async Task<int> Insert(GameUpsertRequest req)
        {
           // var sql = "INSERT INTO Igra (team_1,team_2,team_1_img,team_2_img,score,quote,currentTime,startTime) VALUES (@team_1,@team_2,@team_1_img,@team_2_img,@score,@quote,startTime)";
            var sql = "INSERT INTO Igra (team_1,team_2,score) VALUES (@team_1,@team_2,@score)";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql, req);
                return res;
            }
        }

        public override async Task<List<Game>> GetAll(GameSearchRequest req=null)
        {
            var sql = "SELECT * FROM Igra";

            if (req != null)
            {
                if (req.id != 0)

                    sql += " WHERE";
                if (!string.IsNullOrEmpty(req.team_1))
                    sql += " team_1 = @team_1";
                if (!string.IsNullOrEmpty(req.team_2))
                    sql += " AND team_2 = @team_2";
                //    if (req.start!=null)
                //        sql += " AND start = @start";
                //    if (req.end != null)
                //        sql += " AND end = @end";               
            }

            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.QueryAsync<Igra>(sql, req);
                return mapper.Map<List<Game>>(res.ToList());
            }
        }

    }
}
