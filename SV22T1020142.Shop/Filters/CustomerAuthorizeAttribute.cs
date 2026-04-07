using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SV22T1020142.Shop.AppCodes;

namespace SV22T1020142.Shop.Filters
{
    public class CustomerAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (CustomerSessionHelper.IsLoggedIn(context.HttpContext))
            {
                base.OnActionExecuting(context);
                return;
            }

            if (context.Controller is Controller controller)
            {
                controller.TempData["Error"] = "Vui lòng đăng nhập để tiếp tục.";
            }

            string returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
        }
    }
}
