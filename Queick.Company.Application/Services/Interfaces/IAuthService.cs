using Queick.Company.Application.DTOs.Auth;

namespace Queick.Company.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<UserDto> RegisterAsync(RegisterRequestDto request);
    Task LogoutAsync(Guid userId, string refreshToken);
    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}