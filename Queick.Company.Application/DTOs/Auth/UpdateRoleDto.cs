namespace Queick.Company.Application.DTOs.Auth;

public class UpdateRoleDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public List<string> PermissionCodes { get; set; }
}