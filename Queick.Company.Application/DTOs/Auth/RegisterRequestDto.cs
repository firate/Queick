namespace Queick.Company.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<long> RoleIds { get; set; }
}