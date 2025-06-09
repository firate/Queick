using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtService _jwtService;
    private readonly ITokenCacheService _tokenCacheService;
    private readonly IConfiguration _configuration;
    
    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtService jwtService,
        ITokenCacheService tokenCacheService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
        _tokenCacheService = tokenCacheService;
        _configuration = configuration;
    }
    
    public async Task<AuthResult> AuthenticateAsync(string username, string password)
    {
        try
        {
            var result = await LoginAsync(new LoginRequestDto 
            { 
                Username = username, 
                Password = password 
            });
            return AuthResult.SuccessResult(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return AuthResult.FailureResult(ex.Message);
        }
        catch (Exception ex)
        {
            return AuthResult.FailureResult("Authentication failed");
        }
    }
    
    public async Task<AuthResult> ValidateTokenAsync(string token)
    {
        try
        {
            var isValid = await _jwtService.ValidateToken(token);
            if (!isValid)
            {
                return AuthResult.FailureResult("Invalid token");
            }
            
            var userId = _jwtService.GetUserIdFromToken(token);
            var tokenVersion = _jwtService.GetTokenVersionFromToken(token);
            
            var isVersionValid = await _tokenCacheService.IsTokenValidAsync(long.Parse(userId), tokenVersion);
            if (!isVersionValid)
            {
                return AuthResult.FailureResult("Token has been revoked");
            }
            
            var user = await _userRepository.GetUserWithPermissionsAsync(long.Parse(userId));
            
            return new AuthResult
            {
                Success = true,
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Permissions = _jwtService.GetPermissionsFromToken(token)
            };
        }
        catch (Exception)
        {
            return AuthResult.FailureResult("Token validation failed");
        }
    }
    
    public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var result = await RefreshTokenAsync(new RefreshTokenRequestDto 
            { 
                RefreshToken = refreshToken 
            });
            return AuthResult.SuccessResult(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return AuthResult.FailureResult(ex.Message);
        }
        catch (Exception)
        {
            return AuthResult.FailureResult("Token refresh failed");
        }
    }
    
    public async Task RevokeTokenAsync(long userId, string refreshToken)
    {
        await LogoutAsync(userId, refreshToken);
    }
    
    public string GetProviderName() => "JWT";
    
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        
        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("User account is inactive");
        }
        
        // Generate token version
        var tokenVersion = Guid.NewGuid().ToString();
        
        // Store token version in Redis
        var tokenExpiration = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"]));
        await _tokenCacheService.SetUserTokenVersionAsync(user.Id, tokenVersion, tokenExpiration);
        
        // Generate tokens
        var accessToken = await _jwtService.GenerateAccessToken(user.Id, tokenVersion);
        var refreshToken = await _jwtService.GenerateRefreshToken();
        
        // Save refresh token
        await _refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            CreatedAt = DateTime.UtcNow
        });
        
        // Get user with permissions for response
        var userWithPermissions = await _userRepository.GetUserWithPermissionsAsync(user.Id);
        
        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Roles = userWithPermissions.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Permissions = userWithPermissions.UserRoles
                    .SelectMany(ur => ur.Role.RolePermissions)
                    .Select(rp => rp.Permission.Code)
                    .Distinct()
                    .ToList()
            }
        };
    }
    
    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        if (refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }
        
        // Revoke old refresh token
        await _refreshTokenRepository.RevokeTokenAsync(request.RefreshToken);
        
        // Generate new token version
        var tokenVersion = Guid.NewGuid().ToString();
        
        // Store new token version in Redis
        var tokenExpiration = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"]));
        await _tokenCacheService.SetUserTokenVersionAsync(refreshToken.UserId, tokenVersion, tokenExpiration);
        
        // Generate new tokens
        var accessToken = await _jwtService.GenerateAccessToken(refreshToken.UserId, tokenVersion);
        var newRefreshToken = await _jwtService.GenerateRefreshToken();
        
        // Save new refresh token
        await _refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = refreshToken.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            CreatedAt = DateTime.UtcNow
        });
        
        // Get user with permissions
        var user = await _userRepository.GetUserWithPermissionsAsync(refreshToken.UserId);
        
        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Permissions = user.UserRoles
                    .SelectMany(ur => ur.Role.RolePermissions)
                    .Select(rp => rp.Permission.Code)
                    .Distinct()
                    .ToList()
            }
        };
    }
    
    public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
    {
        // Check if user exists
        if (await _userRepository.UserExistsAsync(request.Username, request.Email))
        {
            throw new InvalidOperationException("Username or email already exists");
        }
        
        // Create user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            IsActive = true
        };
        
        await _userRepository.CreateAsync(user);
        
        // Assign roles
        if (request.RoleIds != null && request.RoleIds.Any())
        {
            foreach (var roleId in request.RoleIds)
            {
                user.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });
            }
            await _userRepository.UpdateAsync(user);
        }
        
        // Get user with permissions
        var userWithPermissions = await _userRepository.GetUserWithPermissionsAsync(user.Id);
        
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = userWithPermissions.UserRoles.Select(ur => ur.Role.Name).ToList(),
            Permissions = userWithPermissions.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Code)
                .Distinct()
                .ToList()
        };
    }
    
    public async Task LogoutAsync(long userId, string refreshToken)
    {
        // Revoke refresh token
        await _refreshTokenRepository.RevokeTokenAsync(refreshToken);
        
        // Invalidate token in Redis
        await _tokenCacheService.InvalidateUserTokenAsync(userId);
    }
    
    public async Task<bool> ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null || !VerifyPassword(currentPassword, user.PasswordHash))
        {
            return false;
        }
        
        user.PasswordHash = HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
        
        // Invalidate all tokens
        await _refreshTokenRepository.RevokeAllUserTokensAsync(userId);
        await _tokenCacheService.InvalidateUserTokenAsync(userId);
        
        return true;
    }
    
    private string HashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            
        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }
    
    private bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split('.');
        if (parts.Length != 2) return false;
        
        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];
        
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            
        return hash == hashed;
    }
}