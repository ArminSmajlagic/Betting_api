using Betting_api.DB_Models;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.IBaseRepos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evona_hackathon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class GameController : BaseCrudController<Game, Igra, GameSearchRequest, GameUpsertRequest>
    {
        public GameController(ICrudRepo<Game, Igra, GameSearchRequest, GameUpsertRequest> service) : base(service)
        {

        }
    }
}
