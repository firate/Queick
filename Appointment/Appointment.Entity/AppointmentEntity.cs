namespace Appointment.Entity;

public class AppointmentEntity : BaseEntity
{
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Location Location { get; set; }
    public int LocationId { get; set; }
    
    
    
}

public class AppointmentProvider: BaseEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
}