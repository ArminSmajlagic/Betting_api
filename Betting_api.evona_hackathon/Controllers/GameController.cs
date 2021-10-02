using Betting_api.DB_Models;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.BaseRepos;
using evona_hackathon.Services.IBaseRepos;
using evona_hackathon.Services.IRepos;
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

        private readonly IGameRepo service;

        public GameController(IGameRepo service) : base(service)
        {
            this.service = service;
        }

        [HttpGet]
        public override async Task<List<Game>> GetAll(GameSearchRequest obj)
        {
            return await service.GetAll(obj);
        }
    }
}
