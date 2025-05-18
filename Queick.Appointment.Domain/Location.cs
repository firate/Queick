using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Location : IEntity
{
    public long CompanyId { get; set; }
    public Company? Company { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public long Id { get; set; }
}