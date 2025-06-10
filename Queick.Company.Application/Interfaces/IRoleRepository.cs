using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IRoleRepository: IBaseRepository<Role>
{
    Task<Role> GetRoleWithPermissionsAsync(long roleId);
    Task<List<Role>> GetAllAsync();
}