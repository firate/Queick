namespace Company.Entity;

public class Branch: BaseEntity
{
    public CompanyEntity Company { get; set; }
    public long CompanyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public bool IsPrimary { get; set; }
    
}