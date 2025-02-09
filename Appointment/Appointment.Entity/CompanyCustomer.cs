namespace Appointment.Entity;

public class CompanyCustomer
{
    public Company Company { get; set; }
    public long CompanyId { get; set; }
    
    public Customer Customer { get; set; }
    public long CustomerId { get; set; }
}