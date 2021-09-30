using evona_hackathon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using evona_hackathon.Services.Services;
using evona_hackathon.Services.IBaseRepos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;

namespace evona_hackathon.Services.BaseRepos
{
    public class CrudRepo<T, Tdb, TSearch, TUpsert> :ReadRepo<T, Tdb, TSearch>,ICrudRepo<T, Tdb, TSearch, TUpsert> where T : class where Tdb : class where TUpsert : class where TSearch:class
    {
        //private readonly DB_Context db;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly string typeName;

        //public CrudRepo(DB_Context db,IMapper mapper,IConfiguration config,ILogger logger):base(db,mapper, config)
        public CrudRepo(IMapper mapper,IConfiguration config,ILogger _logger):base(mapper, config,_logger)
        {
            //this.db = db;
            this.mapper = mapper;
            this.config = config;
            this.logger = _logger;
            typeName = typeof(Tdb).Name;
        }

        //not fully implemented - explanation bellow the current implementation
        public virtual async Task<int> Insert(TUpsert request)
        {
            var sql = "INSERT INTO " + typeName;
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql,request);
                return res;
            }

            //generic insert does not have simple implementation
            //each repository / service will need overriden implementation 
        }

        //not fully implemented - explanation bellow the current implementation

        public virtual async Task<int> Update(int id, TUpsert request)
        {
            var sql = "UPDATE " + typeName + "SET ";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql,request);
                return res;
            }
            //generic update does not have simple implementation
            //each repository / service will need overriden implementation 
        }

        public virtual async Task<int> Delete(int id)
        {
            var sql = "DELETE " + typeName + " WHERE id = @id";
            using (var connection = new SqlConnection(config.GetConnectionString("pg_db")))
            {
                connection.Open();
                var res = await connection.ExecuteAsync(sql,new { id=id });
                return res;
            }
        }


        // sql + EF methods (not async)

        //public virtual T Insert(TUpsert request)
        //{
        //    var set = db.Set<Tdb>();

        //    Tdb entity = mapper.Map<Tdb>(request);

        //    set.Add(entity);

        //    db.SaveChanges();

        //    return mapper.Map<T>(entity);
        //}

        //public virtual T Update(int id, TUpsert request)
        //{
        //    var set = db.Set<Tdb>();

        //    var entity = set.Find(id);

        //    mapper.Map(request, entity);

        //    db.SaveChanges();

        //    return mapper.Map<T>(entity);
        //}

    }
}
