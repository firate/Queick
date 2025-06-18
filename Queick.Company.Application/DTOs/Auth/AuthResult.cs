namespace Queick.Company.Application.DTOs.Auth;

public class AuthResult
{
    public bool Success { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid? UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Permissions { get; set; }
    public string ErrorMessage { get; set; }
    
    public static AuthResult SuccessResult(LoginResponseDto loginResponse)
    {
        return new AuthResult
        {
            Success = true,
            AccessToken = loginResponse.AccessToken,
            RefreshToken = loginResponse.RefreshToken,
            ExpiresAt = loginResponse.ExpiresAt,
            UserId = loginResponse.User.Id,
            Username = loginResponse.User.Username,
            Email = loginResponse.User.Email,
            Roles = loginResponse.User.Roles,
            Permissions = loginResponse.User.Permissions
        };
    }
    
    public static AuthResult FailureResult(string errorMessage)
    {
        return new AuthResult
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}