using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;


namespace StandingOrders.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var error = filterContext.Exception.ToString();

            string message = ($"{Environment.NewLine}" +
                $"Controller: {controllerName},{Environment.NewLine}" +
                $"Action: {actionName},{Environment.NewLine}" +
                $"Error: {error}");
            
            filterContext.ExceptionHandled = true;

            filterContext.Result = new ObjectResult("An error has occurred during the execution of the program.")
            {
                StatusCode = 500,
            };

            _logger.LogError(message);
        }

    }
}



