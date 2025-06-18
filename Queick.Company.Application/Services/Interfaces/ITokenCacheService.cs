namespace Queick.Company.Application.Services.Interfaces;

public interface ITokenCacheService
{
    Task<string> GetUserTokenVersionAsync(Guid userId);
    Task SetUserTokenVersionAsync(Guid userId, string tokenVersion, TimeSpan? expiration = null);
    Task InvalidateUserTokenAsync(Guid userId);
    Task<bool> IsTokenValidAsync(Guid userId, string tokenVersion);
}