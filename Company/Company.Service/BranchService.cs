using Company.Data;
using Company.Entity;

namespace Service;

public class BranchService : IBranchService
{
    private readonly CompanyDbContext _companyDbContext;

    public BranchService(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<Branch> CreateBranchAsync(string name, string description)
    {
        Branch branch = new Branch()
        {
            Name = name,
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
        throw new NotImplementedException();
    }

    public async Task<Branch> UpdateBranchAsync(long id, string name, string description)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBranchAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Branch>> GetBranchesAsync(string name, string description, int page, int pageSize,
        string sortBy, string sortDirection)
    {
        throw new NotImplementedException();
    }
}