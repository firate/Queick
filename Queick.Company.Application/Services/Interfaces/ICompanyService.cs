
using Queick.Company.Application.Common.Models;

namespace Queick.Company.Application.Services.Interfaces;


public interface ICompanyService
{
    Task<CompanyDto> GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CompanyDto>> GetCompaniesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task UpdateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task DeleteCompanyAsync(long id, CancellationToken cancellationToken = default);
}