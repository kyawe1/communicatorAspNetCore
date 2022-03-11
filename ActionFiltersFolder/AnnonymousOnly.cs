using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace communicator.ActionFiltersFolder;


public class AnnonymousOnly : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectResult("/Home/Index");
        }
        
    }
}