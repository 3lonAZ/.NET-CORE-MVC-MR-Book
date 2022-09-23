using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace MR_Book.Models.Filter
{
    public class AdminFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var adminUser = context.HttpContext.Session.GetString("admin_usr");
            if (string.IsNullOrEmpty(adminUser))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"action","Index" },
                    {"controller","Admin"}
                });
            }
            base.OnActionExecuting(context);    
        }
    }
}
