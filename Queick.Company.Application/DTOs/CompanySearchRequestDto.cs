namespace Queick.Company.Application.Common.Models;

public class CompanySearchRequestDto: BaseSearchRequestDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}