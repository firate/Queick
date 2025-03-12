namespace Appointment.Entity;

public class CompanyCustomer
{
    public required Company Company { get; set; }
    public long CompanyId { get; set; }
    
    public required Customer Customer { get; set; }
    public long CustomerId { get; set; }
}