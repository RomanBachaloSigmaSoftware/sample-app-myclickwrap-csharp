using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocuSign.MyClick.COVID19Waiver
{
    public class ContextFilter : IActionFilter
    {
        private readonly Context _context;

        public ContextFilter(Context context)
        {
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                _context.Init(httpContext.User);
            }
        }
    }
}