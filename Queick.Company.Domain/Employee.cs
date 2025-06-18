using Queick.Company.Domain.Common;

namespace Queick.Company.Domain;

public class Employee : Entity, ISoftDeleteEntity, IActivatable, IAuditableEntity
{
    //public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public bool IsActive { get; set; }
   
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    
    public List<Address> Addresses { get; set; } = [];
    
    public Branch? Branch { get; set; }
    public long BranchId { get; set; }
    
    public List<CommunicationInfo> CommunicationInfos { get; set; } = [];
}