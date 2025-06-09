using Queick.Company.Application.DTOs.Auth;

namespace Queick.Company.Application.Services.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(long id);
    Task<RoleDto> CreateRoleAsync(CreateRoleDto dto);
    Task<RoleDto> UpdateRoleAsync(UpdateRoleDto dto);
    Task<bool> DeleteRoleAsync(long id);
    Task<List<PermissionDto>> GetAllPermissionsAsync();
}