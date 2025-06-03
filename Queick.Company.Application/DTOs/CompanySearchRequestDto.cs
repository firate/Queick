using Queick.Company.Application.Common;

namespace Queick.Company.Application.DTOs;

public class CompanySearchRequestDto: BaseSearchRequestDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool OnlyActives { get; set; }
    public bool OnlyDeleteds { get; set; }
    public DateTimeOffset? CreatedFrom { get; set; }
    public DateTimeOffset? CreatedTo { get; set; }
    
}