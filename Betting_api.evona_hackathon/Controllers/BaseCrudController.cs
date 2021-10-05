using evona_hackathon.Services.IBaseRepos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evona_hackathon.API.Controllers
{
    [ApiController]
    //[BaseResourcesFilter]
    //[BaseActionFilter]
    public class BaseCrudController<T,Tdb,TSearch,TUpsert>:Controller
    {
        private readonly ICrudRepo<T, Tdb, TSearch, TUpsert> service;

        public BaseCrudController(ICrudRepo<T, Tdb, TSearch, TUpsert> service)
        {
            this.service = service;
        }

        [HttpGet]
        public virtual async Task<List<T>> GetAll([FromQuery] TSearch q)
        {
            return await service.GetAll(q);
        }

        [HttpGet("{id}")]
        public virtual async Task<T> GetByID(int id)
        {
            return await service.GetById(id);
        }

        [HttpPut("{id}")]
        public virtual async Task<int> Update(int id, [FromBody] TUpsert entity)
        {
            return await service.Update(id, entity);
        }

        [HttpPost]
        public virtual async Task<int> Insert([FromBody]TUpsert entity)
        {
            return await service.Insert(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<int> Delete(int id)
        {
            return await service.Delete(id);
        }
    }
}
