using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensoStatWeb.WebApplication.Filters
{
    public class DefaultActionFilter : IActionFilter, IExceptionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("======= ERROR =======");

            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = "Error"
            }));

            Console.WriteLine("=====================");
        }
    }
}
