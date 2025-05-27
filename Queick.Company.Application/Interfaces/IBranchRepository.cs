using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<List<Branch>> GetBranchesByCompanyIdAsync(long companyId, CancellationToken cancellationToken = default);
    Task<List<Branch>> GetActiveBranchesAsync(CancellationToken cancellationToken = default);
    Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId, long? excludeId = null, CancellationToken cancellationToken = default);
}