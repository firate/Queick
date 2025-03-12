namespace Appointment.Entity;

public class District : BaseEntity
{
    public required string Name { get; set; }
    public Province Province { get; set; }
    public int ProvinceId { get; set; }
}