
using Queick.Company.Domain.Common;

namespace Queick.Company.Domain;

public class CompanyDomain : Entity, ISoftDeleteEntity, IActivatable, IAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public List<Branch> Branches { get; set; } = [];
}