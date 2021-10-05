using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    public class BaseActionFilter:ActionFilterAttribute
    {
        //"sandwiched" between resources filter executing and executed methods
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Base action filter has ben called upon...and it is executed!");

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Base action filter has ben called upon...and it is executing!");
        }
    }
}
