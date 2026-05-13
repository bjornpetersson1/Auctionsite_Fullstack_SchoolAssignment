using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auctionsite_Backend.Filters
{
    public class RequireActiveUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isActive = context.HttpContext.User.FindFirst("IsActive")?.Value;
            if (isActive != "true")
            {
                context.Result = new BadRequestObjectResult("User inactvated");
            }
        }
    }
}
