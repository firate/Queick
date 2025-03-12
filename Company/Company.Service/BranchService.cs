using Company.Data;
using Company.Entity;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class BranchService : IBranchService
{
    private readonly CompanyDbContext _companyDbContext;

    public BranchService(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<Branch> CreateBranchAsync(string name, long companyId, string description)
    {
        var company = await _companyDbContext.Companies.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == companyId);

        if (company == null)
        {
            throw new ArgumentNullException(nameof(company));
        }
            
        Branch branch = new Branch()
        {
            Name = name,
            CompanyId = company.Id,
            Company = company,
            Description = description
        };
        _companyDbContext.Branches.Add(branch);
        var isSaved = await _companyDbContext.SaveChangesAsync();
        if (isSaved > 0)
        {
            return branch;
        }

        return null;
    }

    public async Task<Branch> GetBranchAsync(long id)
    {
        var branch = await _companyDbContext.Branches.FindAsync(id);
        
        return branch;
    }

    public async Task<Branch> UpdateBranchAsync(long id, string name, string description)
    {
        var branch = await _companyDbContext.Branches.FindAsync(id);
        
        if (branch == null)
        {
            return null;
        }
        
        branch.Name = name;
        branch.Description = description;
        
        var isSaved = await _companyDbContext.SaveChangesAsync();
        if (isSaved > 0)
        {
            return branch;
        }

        return null;
    }

    public async Task<bool> DeleteBranchAsync(long id)
    {
        var branch = await _companyDbContext.Branches.FindAsync(id);
        
        if (branch == null)
        {
            return false;
        }
        
        branch.IsDeleted = true;
        
        var isUpdated = await _companyDbContext.SaveChangesAsync();
        if (isUpdated <= 0) return false;
        return true;
    }

    public async Task<IEnumerable<Branch>> GetBranchesAsync(string name, string description, int page, int pageSize,
        string sortBy, string sortDirection)
    {
        var branches = _companyDbContext.Branches.AsQueryable();
        
        if (!string.IsNullOrEmpty(name))
        {
            branches = branches.Where(x => x.Name.Contains(name));
        }
        
        if (!string.IsNullOrEmpty(description))
        {
            branches = branches.Where(x => x.Description.Contains(description));
        }
        
        // if (!string.IsNullOrEmpty(sortBy))
        // {
        //     branches = branches.OrderBy(sortBy);
        // }
        //
        // if (!string.IsNullOrEmpty(sortDirection))
        // {
        //     branches = branches.OrderBy(sortDirection);
        // }
        
        var totalCount = await _companyDbContext.Branches.CountAsync();
        
        var branchList= branches.Skip(page * pageSize).Take(pageSize);

        return branchList;
    }
}