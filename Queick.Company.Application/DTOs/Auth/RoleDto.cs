namespace Queick.Company.Application.DTOs.Auth;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public List<string> Permissions { get; set; }
}