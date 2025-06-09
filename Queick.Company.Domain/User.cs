namespace Queick.Company.Domain;

public class User : IEntity, IAuditableEntity, IActivatable
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}