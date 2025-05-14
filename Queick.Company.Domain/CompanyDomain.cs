namespace Queick.Company.Domain;

public class CompanyDomain: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public List<Branch> Branches { get; set; } = new();
}