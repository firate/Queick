namespace Queick.Company.Application.DTOs;

public class BranchCreationDto
{
    public long CompanyId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
}