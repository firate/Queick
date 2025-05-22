using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Employee : IEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public Position? Position { get; set; }
    public long PositionId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Branch? Branch { get; set; }
    public long BranchId { get; set; }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    
    public long Id { get; set; }
}