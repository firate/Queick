namespace Queick.Company.Application.Common.Models;

public abstract class BaseSearchRequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}
public class CompanySearchRequestDto: BaseSearchRequestDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
public class CompanyCreationDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class CompanyUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}

public class CompanyDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}


public class CompanyMultipleUpdateResultDto
{
    public long Id { get; set; }
    public string? ErrorMessage { get; set; }
}


public class CompanyDeleteResultDto
{
    public long Id { get; set; }
    public string? ErrorMessage { get; set; }
}

