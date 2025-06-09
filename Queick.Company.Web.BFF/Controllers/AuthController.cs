using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.Authorization;
using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Web.BFF.Attributes;

namespace Queick.Company.Web.BFF.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        try
        {
            var response = await _authService.RefreshTokenAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    
    [HttpPost("register")]
    [Authorize]
    [RequirePermission(Permissions.User.Write)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _authService.LogoutAsync(userId, request.RefreshToken);
        return Ok(new { message = "Logged out successfully" });
    }
    
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _authService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);
        
        if (!result)
        {
            return BadRequest(new { message = "Current password is incorrect" });
        }
        
        return Ok(new { message = "Password changed successfully" });
    }
}

