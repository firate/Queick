namespace Queick.Company.Application.DTOs.Auth;

public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Permissions { get; set; }
}