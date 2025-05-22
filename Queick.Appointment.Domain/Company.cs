using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Company: IEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public List<Branch> Branches { get; set; } = new();
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}