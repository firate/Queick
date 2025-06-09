using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Queick.Company.Application.Common;

namespace Queick.Company.Application.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId ?? "System"; // Return "System" for non-authenticated requests
    }
    
    public List<string> GetCurrentUserPermissions()
    {
        var permissions = _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .ToList();
            
        return permissions ?? new List<string>();
    }
    
    public bool HasPermission(string permission)
    {
        return GetCurrentUserPermissions().Contains(permission);
    }
    
    public bool HasAnyPermission(params string[] permissions)
    {
        var userPermissions = GetCurrentUserPermissions();
        return permissions.Any(p => userPermissions.Contains(p));
    }
    
    public bool HasAllPermissions(params string[] permissions)
    {
        var userPermissions = GetCurrentUserPermissions();
        return permissions.All(p => userPermissions.Contains(p));
    }
}