namespace Domains;

public class Branch: BaseEntity
{
    public Company Company { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}