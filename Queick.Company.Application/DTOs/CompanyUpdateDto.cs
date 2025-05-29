namespace Queick.Company.Application.DTOs;

public class CompanyUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}