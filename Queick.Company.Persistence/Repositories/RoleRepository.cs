using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Persistence.Repositories.Base;

namespace Queick.Company.Persistence.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
    
    public async Task<Role> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }
    
    public async Task<Role> GetRoleWithPermissionsAsync(long roleId)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == roleId);
    }
    
    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }
    
    
}