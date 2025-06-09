namespace Queick.Company.Application.DTOs.Auth;

public class CreateRoleDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> PermissionCodes { get; set; }
}