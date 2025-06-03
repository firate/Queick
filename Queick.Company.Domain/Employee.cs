using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Employee : IEntity, ISoftDeleteEntity, IActivatable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public bool IsActive { get; set; }
   
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    
    // public Address? Address { get; set; }
    // public long AddressId { get; set; }
    
    public List<Address> Addresses { get; set; } = [];
    
    public Branch? Branch { get; set; }
    public long BranchId { get; set; }
    
    public List<CommunicationInfo> CommunicationInfos { get; set; } = [];
}