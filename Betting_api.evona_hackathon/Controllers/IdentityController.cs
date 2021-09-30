using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace evona_hackathon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/Auth")]
    public class IdentityController : Controller
    {
        private readonly IAuthRepo authService;

        public IdentityController(IAuthRepo authService)
        {
            this.authService = authService;
        }

        [HttpPost, Route("/Login")]
        public async Task<IActionResult> Login([FromBody]LoginReq user)
        {
            string res = await authService.Login(user);
            if (user == null)
                return BadRequest("User is null");

            return Ok(res);

        }

        [HttpPost,Route("/Register")]
        public async Task<IActionResult> Register([FromBody]RegisterReq user)
        {
            string result = await authService.Register(user);
             return Ok(result);
        }
    }
}
