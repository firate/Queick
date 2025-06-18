using Queick.Company.Domain.Common;

namespace Queick.Company.Domain;

public class Role : Entity, IAuditableEntity, IActivatable
{
    // public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public bool IsActive { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}