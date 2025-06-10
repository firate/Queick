using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Persistence.Repositories.Base;

namespace Queick.Company.Persistence.Repositories;

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context): base(context)
    {
    }
    
    public async Task<List<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }
    
    public async Task<List<Permission>> GetByCodesAsync(List<string> codes)
    {
        return await _context.Permissions
            .Where(p => codes.Contains(p.Code))
            .ToListAsync();
    }
    
}