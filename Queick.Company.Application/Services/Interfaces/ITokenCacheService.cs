namespace Queick.Company.Application.Services.Interfaces;

public interface ITokenCacheService
{
    Task<string> GetUserTokenVersionAsync(long userId);
    Task SetUserTokenVersionAsync(long userId, string tokenVersion, TimeSpan? expiration = null);
    Task InvalidateUserTokenAsync(long userId);
    Task<bool> IsTokenValidAsync(long userId, string tokenVersion);
}