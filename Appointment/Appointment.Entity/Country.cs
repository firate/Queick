namespace Appointment.Entity;

public class Country : BaseEntity
{
    public required string Name { get; set; }
    public required string IsoCode { get; set; }
}