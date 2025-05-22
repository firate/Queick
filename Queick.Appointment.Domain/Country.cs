using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;


public class Country : IEntity
{
    public required string Name { get; set; }
    public required string IsoCode { get; set; }
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}