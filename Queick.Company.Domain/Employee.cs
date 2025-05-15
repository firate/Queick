namespace Queick.Company.Domain;

public class Employee : IEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Branch? Branch { get; set; }
    public long BranchId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public long Id { get; set; }
}