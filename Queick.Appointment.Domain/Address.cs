using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Address : IEntity
{
    public string? Name { get; set; }
    public long Id { get; set; }
}