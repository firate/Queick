using Queick.Company.Application.Common;

namespace Queick.Company.Application.DTOs;

public class BranchSearchRequestDto: BaseSearchRequestDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public long CompanyId { get; set; }
}