using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Persistence.Repositories.Base;

namespace Queick.Company.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    
    public UserRepository(ApplicationDbContext context) : base(context)
    {
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
    
    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username || u.Email == email);
    }
}