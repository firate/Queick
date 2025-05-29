using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface ICompanyRepository : IBaseRepository<CompanyDomain>
{
    Task<CompanyDomain?> GetCompanyWithBranchesAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> IsCompanyNameExistsAsync(string name, CancellationToken cancellationToken = default);
}