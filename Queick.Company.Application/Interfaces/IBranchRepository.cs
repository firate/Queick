using Queick.Company.Application.DTOs;
using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<List<Branch>> GetBranchesByCompanyIdAsync(long companyId, CancellationToken cancellationToken = default);

    Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId,
        CancellationToken cancellationToken = default);

    Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        long companyId, 
        string name, 
        string description, 
        bool onlyActiveRecords, 
        int skip,
        int take,
        CancellationToken cancellationToken = default);
}