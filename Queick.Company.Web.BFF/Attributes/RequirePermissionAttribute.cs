using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Queick.Company.Application.Common;

namespace Queick.Company.Web.BFF.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequirePermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _permissions;
    private readonly bool _requireAll;
    
    public RequirePermissionAttribute(params string[] permissions)
    {
        _permissions = permissions;
        _requireAll = false;
    }
    
    public RequirePermissionAttribute(bool requireAll, params string[] permissions)
    {
        _permissions = permissions;
        _requireAll = requireAll;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
        
        bool hasPermission;
        if (_requireAll)
        {
            hasPermission = currentUserService.HasAllPermissions(_permissions);
        }
        else
        {
            hasPermission = currentUserService.HasAnyPermission(_permissions);
        }
        
        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}