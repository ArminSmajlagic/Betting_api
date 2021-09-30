using Betting_api.DB_Models;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.IBaseRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.IRepos
{
    public interface IGameRepo:ICrudRepo<Game,Igra,GameSearchRequest,GameUpsertRequest>
    {
    }
}
