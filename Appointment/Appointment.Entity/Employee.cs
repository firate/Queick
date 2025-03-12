namespace Appointment.Entity;

public class Employee : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    // public string Address { get; set; }
    public required Position Position { get; set; }
    public required long PositionId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public required Branch Branch { get; set; }
    public required long BranchId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}