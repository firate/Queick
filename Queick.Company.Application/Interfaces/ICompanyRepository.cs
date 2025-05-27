using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface ICompanyRepository : IBaseRepository<CompanyDomain>
{
    Task<List<CompanyDomain>> GetActiveCompaniesAsync(CancellationToken cancellationToken = default);
    Task<CompanyDomain?> GetCompanyWithBranchesAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> IsCompanyNameExistsAsync(string name, long? excludeId = null, CancellationToken cancellationToken = default);
}