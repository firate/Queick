namespace Appointment.Entity;

public class AppointmentTemplate : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }
    
}