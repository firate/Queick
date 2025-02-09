namespace Company.Entity;

public class CompanyEntity: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public List<Branch> Branches { get; set; } = new();
}