namespace Queick.Company.Domain;

public class Permission : IEntity
{
    public long Id { get; set; }
    public string Code { get; set; } // e.g., "Company.Read", "Company.Write"
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; } // e.g., "Company", "Branch"
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}