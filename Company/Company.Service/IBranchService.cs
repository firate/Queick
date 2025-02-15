using Company.Entity;

namespace Service;

public interface IBranchService
{
    Task<Branch> CreateBranchAsync(string name, string description);
    
    Task<Branch> GetBranchAsync(long id);    
    Task<Branch> UpdateBranchAsync(long id, string name, string description);
    
    Task<bool> DeleteBranchAsync(long id);
    
    Task<IEnumerable<Branch>> GetBranchesAsync(string name, string description, int page, int pageSize, string sortBy, string sortDirection);
}