namespace Queick.Company.Application.Services.Interfaces;

public interface IJwtService
{
    Task<string> GenerateAccessToken(Guid userId, string tokenVersion);
    Task<string> GenerateRefreshToken();
    Task<bool> ValidateToken(string token);
    string GetUserIdFromToken(string token);
    List<string> GetPermissionsFromToken(string token);
    string GetTokenVersionFromToken(string token);
}