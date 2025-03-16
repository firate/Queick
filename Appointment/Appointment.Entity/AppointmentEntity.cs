namespace Appointment.Entity;

public class AppointmentEntity : BaseEntity
{
    public long CustomerId { get; set; }
    public int LocationId { get; set; }
    public required Location Location { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Schedule? Template { get; set; }
    public long? TemplateId { get; set; }
    /// <summary>
    /// Kullanıcının randevu aldığı şirket çalışanı
    /// </summary>
    public int EmployeeId { get; set; }
    public required Employee Employee { get; set; }
}