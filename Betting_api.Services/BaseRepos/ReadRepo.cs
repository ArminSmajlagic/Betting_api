using AutoMapper;
using Dapper;
using evona_hackathon.Data;
using evona_hackathon.Services.IBaseRepos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.BaseRepos
{
    public class ReadRepo<T,Tdb,TSearch> : IReadRepo<T, Tdb, TSearch>where T :class where Tdb : class where TSearch:class
    {
        //private readonly DB_Context db; - SQL + EF field
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly string typeName;

        //public ReadRepo(DB_Context db, IMapper mapper,IConfiguration config) - SQL + EF Ctor
        public ReadRepo(IMapper _mapper,IConfiguration _config,ILogger _logger)
        {
            //this.db = db;
            this.mapper = _mapper;
            this.config = _config;
            logger = _logger;
            typeName = typeof(Tdb).Name;
        }


        public virtual async Task<List<T>> GetAll(TSearch obj = null)
        {
            var sql = "SELECT * FROM " + typeName;

            using(var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.QueryAsync<Tdb>(sql);
                return mapper.Map<List<T>>(res.ToList());
            }

            //each getAll method should have overriden implementation for the sake of TSearch argument
        }

        public virtual async Task<T> GetById(int id)
        {
            var sql = "SELECT * FROM "+ typeName + " WHERE id = @id";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.QueryAsync(sql, new { id = id });
                return mapper.Map<T>(res.FirstOrDefault());
            }
        }


        //sql + entity framework implementation (not async)

        //public virtual List<T> GetAll(TSearch obj=null)
        //{
        //    var list = db.Set<Tdb>();
        //    var lista = list.ToList();
        //    return  mapper.Map<List<T>>(lista);
        //}

        //public virtual T GetById(int id)
        //{
        //    var entity = db.Set<Tdb>().Find(id);
        //    return mapper.Map<T>(entity);
        //}
    }
}
