namespace Queick.Company.Application.Common;

public abstract class BaseSearchRequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
}