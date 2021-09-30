using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.IBaseRepos
{
    public interface ICrudRepo<T,Tdb,TSearch,TUpsert>:IReadRepo<T, Tdb, TSearch>
    {
        Task<int> Insert(TUpsert request);
        Task<int> Update(int id, TUpsert request);
        Task<int> Delete(int id);
    }
}
