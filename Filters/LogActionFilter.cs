using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace StandingOrders.API.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private Stopwatch timer;
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();

            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();

            string message = ($"{Environment.NewLine}" +
                $"Controller: {controllerName},{Environment.NewLine}" +
                $"Action: {actionName},{Environment.NewLine}" +
                $"Elapsed time: {timer.Elapsed.TotalMilliseconds} ms.");
            
            _logger.LogInformation(message);
        }
    }
}
