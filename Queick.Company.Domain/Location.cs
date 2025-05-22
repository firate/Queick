using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Location : IEntity, ISoftDeleteEntity, IActivatable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";
    public bool IsActive { get; set; }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}