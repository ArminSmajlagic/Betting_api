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
            string jsonRes = JsonSerializer.Serialize(new {username=user.username,password=user.password,token=res,expires=DateTime.Now.AddMinutes(15).ToString()});
            if (user == null)
                return BadRequest("User is null");
            if (res == "2")
                return Unauthorized("Invalid username or password");

            return Ok(jsonRes);

        }

        [HttpPost,Route("/Register")]
        public IActionResult Register([FromBody]RegisterReq user)
        {
             return Ok(authService.Register(user));
        }
    }
}
