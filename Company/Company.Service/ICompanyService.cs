using Domains;
using Service.Helpers;

namespace Service;

public interface ICompanyService
{
    Task<Company> CreateCompanyAsync(string name, string description);
    Task<Company> GetCompanyByIdAsync(long id);
    Task<PaginationResult<Company>> GetCompaniesAsync(string name, string description, int page, int pageSize,
        string sortBy, string sortDirection = "asc");
}