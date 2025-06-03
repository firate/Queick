using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface ICompanyRepository : IBaseRepository<CompanyDomain>
{
    Task<(List<CompanyDomain> Companies, int Count)> GetPagedAsync(
        string name,
        string description,
        int skip,
        int take,
        DateTimeOffset? createdFrom,
        DateTimeOffset? createdTo,
        bool onlyActives = true,
        bool onlyDeletedRecords = false,
        CancellationToken cancellationToken = default);
    Task<CompanyDomain?> GetCompanyByIdWithBranchesAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> IsCompanyNameExistsAsync(string name, CancellationToken cancellationToken = default);
}