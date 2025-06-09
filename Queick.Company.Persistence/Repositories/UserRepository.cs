using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetByIdAsync(long id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<User> GetUserWithPermissionsAsync(long userId)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var user = await GetByIdAsync(id);
        if (user == null) return false;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username || u.Email == email);
    }
}