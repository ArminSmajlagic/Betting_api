using evona_hackathon.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    public class AuthenticationFilter:Attribute, IAuthorizationFilter
    {
        private ILogger logger;
        private IAuthRepo authService;

        public AuthenticationFilter()
        {
 
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger));
            authService = (IAuthRepo)context.HttpContext.RequestServices.GetService(typeof(IAuthRepo));
            var res = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                res = false;

            string token = string.Empty;

            if (res)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (authService.verifyToken(token))
                    res = false;
            }
            
            if(!res)
            {
                context.ModelState.AddModelError("Unauthroized","You are not authorized");
                logger.Log(LogLevel.Error, "Authentication error has occured");
                //not following the dictionary convection
                context.Result = new JsonResult(context.ModelState);
            }

        }
    }
}
