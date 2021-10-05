using Betting_api.DB_Models;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.Filters;
using evona_hackathon.Services.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evona_hackathon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class KorisniciController : BaseCrudController<User, Korisnik, UserSearchRequest, UserUpsertRequest>
    {
        private readonly IUserRepo service;

        public KorisniciController(IUserRepo service):base(service)
        {
            this.service = service;
        }

        //[Authorize]
        [AuthenticationFilter] //custom authentication filter assigned localy
        [HttpGet]
        public override async Task<List<User>> GetAll(UserSearchRequest obj)
        {
            return await service.GetAll(obj);
        }
    }
}
