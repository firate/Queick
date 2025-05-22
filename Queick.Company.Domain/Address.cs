using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Address : IEntity, ISoftDeleteEntity, IActivatable
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsPrimary { get; set; }
    
    public long BranchId { get; set; }
    public Branch? Branch { get; set; }
    
    public long CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public long EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public long LocationId { get; set; }
    public Location? Location { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";

    public bool IsActive { get; set; }
}