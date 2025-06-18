using Microsoft.Extensions.Caching.Distributed;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Infrastructure;

public class TokenCacheService : ITokenCacheService
{
    private readonly IDistributedCache _cache;
    private const string TOKEN_VERSION_PREFIX = "token_version:";
    
    public TokenCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    public async Task<string> GetUserTokenVersionAsync(Guid userId)
    {
        var key = $"{TOKEN_VERSION_PREFIX}{userId}";
        return await _cache.GetStringAsync(key);
    }
    
    public async Task SetUserTokenVersionAsync(Guid userId, string tokenVersion, TimeSpan? expiration = null)
    {
        var key = $"{TOKEN_VERSION_PREFIX}{userId}";
        var options = new DistributedCacheEntryOptions();
        
        if (expiration.HasValue)
        {
            options.SetAbsoluteExpiration(expiration.Value);
        }
        else
        {
            // Default to 24 hours
            options.SetAbsoluteExpiration(TimeSpan.FromHours(24));
        }
        
        await _cache.SetStringAsync(key, tokenVersion, options);
    }
    
    public async Task InvalidateUserTokenAsync(Guid userId)
    {
        var key = $"{TOKEN_VERSION_PREFIX}{userId}";
        await _cache.RemoveAsync(key);
    }
    
    public async Task<bool> IsTokenValidAsync(Guid userId, string tokenVersion)
    {
        var cachedVersion = await GetUserTokenVersionAsync(userId);
        return !string.IsNullOrEmpty(cachedVersion) && cachedVersion == tokenVersion;
    }
}