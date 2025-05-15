namespace Queick.Company.Domain;

public class Branch: IEntity
{
    public required CompanyDomain Company { get; set; }
    public required long CompanyId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; } 
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public bool IsPrimary { get; set; }

    public long Id { get; set; }
}