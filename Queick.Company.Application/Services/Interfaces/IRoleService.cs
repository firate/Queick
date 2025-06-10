using Queick.Company.Application.DTOs.Auth;

namespace Queick.Company.Application.Services.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<RoleDto> GetRoleByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<RoleDto> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken = default);
    Task<RoleDto> UpdateRoleAsync(UpdateRoleDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteRoleAsync(long id, CancellationToken cancellationToken = default);
    Task<List<PermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken = default);
}