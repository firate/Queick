namespace Appointment.Entity;

public class AppointmentEntity : BaseEntity
{
    public long CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public long LocationId { get; set; }
    public Location? Location { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Schedule? Schedule { get; set; }
    public long? ScheduleId { get; set; }
    /// <summary>
    /// Kullanıcının randevu aldığı şirket çalışanı
    /// </summary>
    public long EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}