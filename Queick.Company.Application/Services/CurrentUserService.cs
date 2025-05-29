using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Queick.Company.Application.Common;

namespace Queick.Shared.Application.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //public string? UserId => _httpContextAccessor.HttpContext?.Se  FindFirst(ClaimTypes.NameIdentifier);
    public string? UserId => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


    // Claims'ten userId değerini alma
    // var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    // Veya özel bir claim kullanıyorsanız
    // var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
    
    public IReadOnlyList<string> Permissions =>
        _httpContextAccessor.HttpContext?.User
            .FindAll("permission")
            .Select(c => c.Value)
            .ToList() ?? new List<string>();
}

//   <Shared>\<Queick.Shared.Application>\Authorization\CurrentUserService.cs:277 Cannot resolve symbol 'IHttpContextAccessor'