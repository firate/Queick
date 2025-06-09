using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(long id);
    Task<Role> GetByNameAsync(string name);
    Task<Role> GetRoleWithPermissionsAsync(long roleId);
    Task<List<Role>> GetAllAsync();
    Task<Role> CreateAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task<bool> DeleteAsync(long id);
}