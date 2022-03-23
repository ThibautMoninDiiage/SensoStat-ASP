using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensoStatWeb.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter, IActionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        #region OnException
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("======= ERROR =======");

            Console.WriteLine(context.Exception.Message);

            Console.WriteLine("=====================");
        }
        #endregion
    }
}

