namespace Appointment.Entity;

public class Location : BaseEntity
{
    public long CompanyId { get; set; }
    public Company? Company { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
}