using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IRoleRepository: IBaseRepository<Role>
{
    Task<Role> GetRoleWithPermissionsAsync(Guid roleId);
    Task<List<Role>> GetAllAsync();
}