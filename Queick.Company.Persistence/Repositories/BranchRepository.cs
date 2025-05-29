using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Branch>> GetBranchesByCompanyIdAsync(long companyId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Branch>> GetActiveBranchesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}