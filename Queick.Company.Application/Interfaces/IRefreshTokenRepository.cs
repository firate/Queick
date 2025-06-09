using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken> GetByTokenAsync(string token);
    Task RevokeTokenAsync(string token);
    Task RevokeAllUserTokensAsync(long userId);
}