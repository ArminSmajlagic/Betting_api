using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    public class BaseResourcesFilter : Attribute,IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //called at the end - after action filter
            Console.WriteLine("Base resource filter has ben called upon...and it is executed!");
            //Console.WriteLine(context.HttpContext.Request.Path);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //called before action filter
            Console.WriteLine("Base resource filter has ben called upon...and it is executing!");
            //Console.WriteLine(context.HttpContext.Request.Path);
        }
    }
}
