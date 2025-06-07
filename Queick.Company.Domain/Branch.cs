namespace Queick.Company.Domain;

public class Branch: IEntity, ISoftDeleteEntity, IActivatable, IAuditableEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public CompanyDomain? Company { get; set; }
    public long CompanyId { get; set; }
    public List<CommunicationInfo> CommunicationInfos { get; set; } = [];
    public List<Address> Addresses { get; set; } = [];
    public bool IsActive { get; set; }
}