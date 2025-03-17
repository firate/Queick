using Company.Entity;

namespace Appointment.Service;

public interface IBranchService
{
    Task<Branch> CreateBranchAsync(string name, long companyId, string description);
    
    Task<Branch> GetBranchAsync(long id);    
    Task<Branch> UpdateBranchAsync(long id, string name, string description);
    
    Task<bool> DeleteBranchAsync(long id);
    
    Task<IEnumerable<Branch>> GetBranchesAsync(string name, string description, int page, int pageSize, string sortBy, string sortDirection);
}