using Queick.Company.Application.Common;

namespace Queick.Company.Application.DTOs;

public class BranchDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class BranchSearchRequestDto: BaseSearchRequestDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
