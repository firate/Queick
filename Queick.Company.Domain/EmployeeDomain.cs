namespace Queick.Company.Domain;

public class EmployeeDomain: IEntity
{
    public long Id { get; set; }
    public CompanyDomain Company { get; set; }
    public long CompanyId { get; set; }
}