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

 
    public string? GetCurrentUserId()
    {
       //var userId= _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
       
       //return userId;
       
       return "system-user";
    }


}
