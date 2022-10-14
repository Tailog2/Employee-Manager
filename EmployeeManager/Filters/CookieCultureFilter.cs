using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManager.Filters
{
    public class CookieCultureFilter : ActionFilterAttribute
    {
        public CookieCultureFilter()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? culture;
            if (!context.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Culture", out culture))
            {
            }

            Console.WriteLine(culture);

            base.OnActionExecuting(context);
        }
    }
}
