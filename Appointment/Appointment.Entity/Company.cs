namespace Appointment.Entity;

public class Company: BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public List<Branch> Branches { get; set; } = new();
}