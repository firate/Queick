using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly ITokenCacheService _tokenCacheService;

    // Ref: https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
    // Ref: https://andrewlock.net/exploring-the-asp-net-core-identity-passwordhasher/
    private readonly IPasswordHasher<User> _passwordHasher; // Identity's hasher mechanism
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IJwtService jwtService,
        ITokenCacheService tokenCacheService,
        IConfiguration configuration,
        ILogger<AuthService> logger,
        IPasswordHasher<User> passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _tokenCacheService = tokenCacheService;
        _configuration = configuration;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(u => u.Username.Equals(request.Username));
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        else if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("User account is inactive");
        }

        // Generate token version
        var tokenVersion = Guid.NewGuid().ToString();

        // Store token version in Redis
        var tokenExpiration =
            TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"]));
        await _tokenCacheService.SetUserTokenVersionAsync(user.Id, tokenVersion, tokenExpiration);

        // Generate tokens
        var accessToken = await _jwtService.GenerateAccessToken(user.Id, tokenVersion);
        var generatedRefreshTokenString = await _jwtService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = generatedRefreshTokenString,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            CreatedAt = DateTime.UtcNow
        };

        // Save refresh token
        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        // Get user with permissions for response
        var userWithPermissions = await _unitOfWork.Users.GetUserWithPermissionsAsync(user.Id);

        _logger.LogInformation($"User {user.Username} logged in successfully");

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = generatedRefreshTokenString,
            ExpiresAt =
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
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
        var refreshToken =
            await _unitOfWork.RefreshTokens.GetFirstOrDefaultAsync(rt => rt.Token.Equals(request.RefreshToken));
        if (refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        // Revoke old refresh token
        await _unitOfWork.RefreshTokens.RevokeTokenAsync(request.RefreshToken);

        // Generate new token version
        var tokenVersion = Guid.NewGuid().ToString();

        // Store new token version in Redis
        var tokenExpiration =
            TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"]));
        await _tokenCacheService.SetUserTokenVersionAsync(refreshToken.UserId, tokenVersion, tokenExpiration);

        // Generate new tokens
        var accessToken = await _jwtService.GenerateAccessToken(refreshToken.UserId, tokenVersion);
        var newRefreshToken = await _jwtService.GenerateRefreshToken();

        // Save new refresh token
        await _unitOfWork.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = refreshToken.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            CreatedAt = DateTime.UtcNow
        });

        // Get user with permissions
        var user = await _unitOfWork.Users.GetUserWithPermissionsAsync(refreshToken.UserId);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt =
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
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
        if (await _unitOfWork.Users.UserExistsAsync(request.Username, request.Email))
        {
            throw new InvalidOperationException("Username or email already exists");
        }

        // Create user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            IsActive = true
        };

        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        await _unitOfWork.Users.AddAsync(user);

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

            await _unitOfWork.Users.UpdateAsync(user);
        }
        
        await _unitOfWork.SaveChangesAsync();

        // Get user with permissions
        var userWithPermissions = await _unitOfWork.Users.GetUserWithPermissionsAsync(user.Id);

        _logger.LogInformation($"New user registered: {user.Username}");

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
        await _unitOfWork.RefreshTokens.RevokeTokenAsync(refreshToken);

        // Invalidate token in Redis
        await _tokenCacheService.InvalidateUserTokenAsync(userId);
    }

    public async Task<bool> ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user is null)
        {
            return false;
        }
        
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return false;
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
        await _unitOfWork.Users.UpdateAsync(user);
        // Invalidate all tokens
        await _unitOfWork.RefreshTokens.RevokeAllUserTokensAsync(userId);
        await _unitOfWork.SaveChangesAsync();
        
        await _tokenCacheService.InvalidateUserTokenAsync(userId);

        _logger.LogInformation($"User {userId} changed password");

        return true;
    }
}