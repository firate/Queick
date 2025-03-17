namespace Appointment.Entity;

public class Employee : BaseEntity
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
    public DateTimeOffset Updated { get; set; }
}