using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Authorization;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;
    
    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Permission> GetByIdAsync(long id)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Permission> GetByCodeAsync(string code)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Code == code);
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
    
    public async Task SeedPermissionsAsync()
    {
        var allPermissions = Permissions.GetAllPermissions();
        
        foreach (var (code, name, category, description) in allPermissions)
        {
            var exists = await _context.Permissions.AnyAsync(p => p.Code == code);
            if (!exists)
            {
                _context.Permissions.Add(new Permission
                {
                    Code = code,
                    Name = name,
                    Category = category,
                    Description = description
                });
            }
        }
        
        await _context.SaveChangesAsync();
    }
}