using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensoStatWeb.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter, IActionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<IExceptionFilter> _logger;

        public ExceptionFilter(IHostEnvironment hostEnvironment, ILogger<IExceptionFilter> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        #region OnException
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("======= ERROR =======");

            _logger.LogError(context.Exception.Message);
            _logger.LogCritical(context.Exception.Message);
            
            Console.WriteLine(context.Exception.Message);

            Console.WriteLine("=====================");
        }
        #endregion
    }
}

