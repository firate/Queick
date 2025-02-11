using Company.Entity;

namespace Service;

public class BranchService : IBranchService
{
    public async Task<Branch> CreateBranchAsync(string name, string description)
    {
        
        throw new NotImplementedException();
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
    
    public async Task<IEnumerable<Branch>> GetBranchesAsync(string name, string description, int page, int pageSize, string sortBy, string sortDirection)
    {
        
        throw new NotImplementedException();
    }
}