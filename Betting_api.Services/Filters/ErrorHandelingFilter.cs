using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    public class ErrorHandelingFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is UserException)
            {
                context.ModelState.AddModelError("ERROR",context.Exception.Message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.ModelState.AddModelError("ERROR","Server error has occured.");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            // I made the result with the respect to the context convections
            var list = context.ModelState.Where(x=>x.Value.Errors.Count>0).ToDictionary(x=>x.Key,y=>y.Value.Errors.Select(z=>z.ErrorMessage));
            context.Result = new JsonResult(list);
        }

    }
}
