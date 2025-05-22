using Queick.Company.Domain.Enums;
using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class CommunicationInfo : IEntity, ISoftDeleteEntity, IActivatable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    
    public bool IsPrimary { get; set; }
    
    public long? CompanyDomainId { get; set; }
    public CompanyDomain? CompanyDomain { get; set; }
    
    public long? BranchId { get; set; }
    public Branch? Branch { get; set; }
    
    public long? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public long? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public CommunicationType CommunicationType { get; set; }
    public int CommunicationTypeId
    {
        get => (int) CommunicationType; 
        set => CommunicationType = (CommunicationType) value;
    }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";

    public bool IsActive { get; set; }
}