namespace Queick.Company.Application.DTOs.Auth;

public class PermissionDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}