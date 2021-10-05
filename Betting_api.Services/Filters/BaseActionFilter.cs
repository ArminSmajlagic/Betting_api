using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Filters
{
    public class BaseActionFilter:ActionFilterAttribute
    {
        private readonly ILogger logger;

        public BaseActionFilter(ILogger logger)
        {
            this.logger = logger;
        }

        //"sandwiched" between resources filter executing and executed methods
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            logger.Log(LogLevel.Trace,"Base action filter has ben called upon...and it is executed!");

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.Log(LogLevel.Trace,"Base action filter has ben called upon...and it is executing!");
        }
    }
}
