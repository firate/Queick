using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IRefreshTokenRepository: IBaseRepository<RefreshToken>
{
    Task RevokeTokenAsync(string token);
    Task RevokeAllUserTokensAsync(Guid userId);
}