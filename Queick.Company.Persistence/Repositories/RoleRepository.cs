using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;
    
    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Role> GetByIdAsync(long id)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Id == id);
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
    
    public async Task<Role> CreateAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }
    
    public async Task<Role> UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var role = await GetByIdAsync(id);
        if (role == null) return false;
        
        role.IsActive = false; // Soft delete
        await _context.SaveChangesAsync();
        return true;
    }
}