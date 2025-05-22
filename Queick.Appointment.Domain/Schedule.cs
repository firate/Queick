using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Schedule : IEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }
    public long LocationId { get; set; }
    public Location? Location { get; set; }

    public Employee? Employee { get; set; }
    public long EmployeeId { get; set; }
    
    //StartDate ve EndDate'in UTC'den ne kadar farklı olduğu bilgisini tutar.
    public required int UtcOffSetMinutes { get; set; }

    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}