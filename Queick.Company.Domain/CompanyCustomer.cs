namespace Queick.Company.Domain;

public class CompanyCustomer
{
    public CompanyDomain Company { get; set; }
    public long CompanyId { get; set; }
    
    public Customer Customer { get; set; }
    public long CustomerId { get; set; }
}