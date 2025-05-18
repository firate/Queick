using Appointment.Entity;

namespace Queick.Appointment.Domain;


public class Country : IEntity
{
    public required string Name { get; set; }
    public required string IsoCode { get; set; }
    public long Id { get; set; }
}