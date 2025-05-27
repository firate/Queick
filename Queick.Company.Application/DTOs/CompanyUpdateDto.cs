namespace Queick.Company.Application.Common.Models;

public class CompanyUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}