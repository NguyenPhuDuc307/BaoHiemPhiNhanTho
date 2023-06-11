using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeCustomFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new ForbidResult();
            return;
        }

        context.Result = new StatusCodeResult(403);

        // Kiểm tra các quyền người dùng ở đây
        // Nếu không có quyền, đặt context.Result = new ForbidResult();
    }
}