using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensoStatWeb.WebApplication.Filters
{
    public class DefaultActionFilter : IActionFilter, IExceptionFilter
    {
        #region OnActionExecuted()
        /// <summary>
        /// A user need to have a token for each page even on the login and the error page
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // If the routes are not going on "/" and "/login" and "/Error"
            if(context.HttpContext.Request.Path.Value != "/" 
                && !context.HttpContext.Request.Path.Value.Contains("/login") 
                && !context.HttpContext.Request.Path.Value.Contains("/Error")
                )
            {
                // Get the Token from the Cookies
                var token = context.HttpContext?.Request?.Cookies?.FirstOrDefault(x => x.Key == "Token").Value;

                // If the token is null
                if (token == null)
                    // Redirect to route login
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "login", 
                        action = ""
                    }));
            }
        }
        #endregion

        #region OnActionExecuting()
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
        #endregion

        #region OnException
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("======= ERROR =======");

            context.Result = new RedirectToPageResult("/Error");

            Console.WriteLine("=====================");
        }
        #endregion
    }
}
