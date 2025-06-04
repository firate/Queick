using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Branch>> GetBranchesByCompanyIdAsync(long companyId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        long companyId, 
        string name, 
        string description, 
        bool onlyActiveRecords,
        bool includeDeletedRecords,
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        List<Branch> branches = [];
        
        var query = _context.Branches.Include(b=>b.Company).AsQueryable();

        if (companyId > 0)
        {
            query = query.Where(x=>x.CompanyId == companyId);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            query = query.Where(x => !string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(description));
        }
        
        if (onlyActiveRecords)
        {
            query = query.Where(x=> x.IsActive);
        }

        if (!includeDeletedRecords)
        {
            query = query.Where(x => !x.IsDeleted);
        }
        
        var count = query.Count();
        
        if (count <= 0) return (branches, 0);
        
        
        branches = await query.Skip(skip).Take(take).ToListAsync();
        return (branches, count);
    }


    public async Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}