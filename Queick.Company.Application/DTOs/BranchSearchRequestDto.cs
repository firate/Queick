using Queick.Company.Application.Common;

namespace Queick.Company.Application.DTOs;

public class BranchSearchRequestDto: BaseSearchRequestDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }
    public bool OnlyActives { get; set; }
    public bool IncludeDeletedRecords { get; set; }
    public DateTimeOffset? CreatedFrom { get; set; }
    public DateTimeOffset? CreatedTo { get; set; }
}