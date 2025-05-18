using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Province : IEntity
{
    public required string Name { get; set; }
    public long Id { get; set; }
}