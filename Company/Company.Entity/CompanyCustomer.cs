namespace Company.Entity;

public class CompanyCustomer
{
    public CompanyEntity Company { get; set; }
    public long CompanyId { get; set; }
    
    public Customer Customer { get; set; }
    public long CustomerId { get; set; }
}