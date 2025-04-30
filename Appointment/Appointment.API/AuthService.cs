using System.Security.Claims;
using System.Text;
using Appointment.API.DTOs;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Appointment.API;



public class AuthService: IAuthService
{
    public async Task Login(LoginRequestDto loginRequest)
    {
        
    }
    
    
    private static async Task<string> GenerateJwtToken(string username)
    {
        var handler = new JsonWebTokenHandler();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = "http://localhost",
            Audience = "http://localhost",
            SigningCredentials = creds
        };

        var token = handler.CreateToken(tokenDescriptor);
        
        return await Task.FromResult(token);
    }
}