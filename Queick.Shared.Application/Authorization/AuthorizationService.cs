using Microsoft.AspNetCore.Http;
using Queick.Shared.Application.Common;
using Queick.Shared.Domain.Authorization;

namespace Queick.Shared.Application.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICurrentUserService _currentUserService;
    
    public AuthorizationService(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
    {
        _httpContextAccessor = httpContextAccessor;
        _currentUserService = currentUserService;
    }
    
    public Task<bool> IsAuthorizedAsync(IEnumerable<Permission> permissions)
    {
        var userPermissions = _currentUserService.Permissions;
        var requiredPermissions = permissions.Select(p => p.Name).ToList();
        
        return Task.FromResult(
            requiredPermissions.Count == 0 || 
            requiredPermissions.Any(requiredPermission => 
                userPermissions.Contains(requiredPermission)));
    }
    
    public async Task<Result> AuthorizeAsync(IEnumerable<Permission> permissions)
    {
        var isAuthorized = await IsAuthorizedAsync(permissions);
        
        return isAuthorized 
            ? Result.Success() 
            : Result.Failure("Bu işlemi gerçekleştirmek için yetkiniz bulunmamaktadır.");
    }
}