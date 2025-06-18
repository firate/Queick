using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Queick.Company.Application.Extensions;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Web.BFF.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    
    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token != null)
        {
            await AttachUserToContext(context, token);
        }
        
        await _next(context);
    }
    
    private async Task AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
            var tokenVersion = jwtToken.Claims.FirstOrDefault(x => x.Type == "tokenVersion")?.Value;
            
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(tokenVersion))
            {
                // Validate token version in Redis
                var tokenCacheService = context.RequestServices.GetRequiredService<ITokenCacheService>();
                var isValid = await tokenCacheService.IsTokenValidAsync(userId.ToGuid(), tokenVersion);
                
                if (!isValid)
                {
                    // Token version mismatch - token has been revoked
                    return;
                }
            }
            
            // Token is valid, claims will be automatically set by the authentication middleware
        }
        catch
        {
            // Token validation failed
            // Do nothing - user will not be authenticated
        }
    }
}