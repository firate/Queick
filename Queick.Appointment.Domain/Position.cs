using Appointment.Entity;

namespace Queick.Appointment.Domain;

public class Position : IEntity
{
    public required string Name { get; set; }
    public long Id { get; set; }
}