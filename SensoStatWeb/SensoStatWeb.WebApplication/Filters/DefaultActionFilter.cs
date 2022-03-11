using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensoStatWeb.WebApplication.Filters
{
    public class DefaultActionFilter : IActionFilter, IExceptionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.HttpContext.Request.Path.Value != "/" 
                && !context.HttpContext.Request.Path.Value.Contains("/login") 
                && !context.HttpContext.Request.Path.Value.Contains("/Error")
                )
            {
                var token = context.HttpContext?.Request?.Cookies?.FirstOrDefault(x => x.Key == "Token").Value;

                if (token == null)
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "login", 
                        action = ""
                    }));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("======= ERROR =======");

            context.Result = new RedirectToPageResult("/Error");

            Console.WriteLine("=====================");
        }
    }
}
