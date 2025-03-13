namespace Appointment.Entity;

public class AppointmentTemplate : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }
    public long LocationId { get; set; }
    public required Location Location { get; set; }
    //StartDate ve EndDate'in UTC'den ne kadar farklı olduğu bilgisini tutar.
    public required TimeSpan UtcOffSet { get; set; }
    
}