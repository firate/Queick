
using Queick.Company.Application.Common.Models;

namespace Queick.Company.Application.Services.Interfaces;

public interface ICompanyService
{
   // Task<CompanyEntity> CreateCompanyAsync(string name, string description);
    Task<CompanyDto> GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default);
    // Task<PaginatedList<CompanyEntity>> GetCompaniesAsync(string name, string description, int page, int pageSize,
    //     string sortBy, string sortDirection = "asc");
    //
    // // update company
    // Task<CompanyEntity> UpdateCompanyAsync(long id, string name, string description);
    //
    // // delete company
    // Task<bool> DeleteCompanyAsync(long id);    
}