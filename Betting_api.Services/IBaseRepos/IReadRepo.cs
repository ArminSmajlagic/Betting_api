using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.IBaseRepos
{
    public interface IReadRepo<T,Tdb, TSearch>
    {
        Task<List<T>> GetAll(TSearch obj);
        Task<T> GetById(int id);
    }
}
