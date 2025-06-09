using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    
    public RoleService(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }
    
    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        var roleDtos = new List<RoleDto>();
        
        foreach (var role in roles)
        {
            var roleWithPermissions = await _roleRepository.GetRoleWithPermissionsAsync(role.Id);
            roleDtos.Add(new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                Permissions = roleWithPermissions.RolePermissions
                    .Select(rp => rp.Permission.Code)
                    .ToList()
            });
        }
        
        return roleDtos;
    }
    
    public async Task<RoleDto> GetRoleByIdAsync(long id)
    {
        var role = await _roleRepository.GetRoleWithPermissionsAsync(id);
        if (role == null) return null;
        
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            Permissions = role.RolePermissions
                .Select(rp => rp.Permission.Code)
                .ToList()
        };
    }
    
    public async Task<RoleDto> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = new Role
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true
        };
        
        await _roleRepository.CreateAsync(role);
        
        // Add permissions
        if (dto.PermissionCodes != null && dto.PermissionCodes.Any())
        {
            var permissions = await _permissionRepository.GetByCodesAsync(dto.PermissionCodes);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                });
            }
            await _roleRepository.UpdateAsync(role);
        }
        
        return await GetRoleByIdAsync(role.Id);
    }
    
    public async Task<RoleDto> UpdateRoleAsync(UpdateRoleDto dto)
    {
        var role = await _roleRepository.GetRoleWithPermissionsAsync(dto.Id);
        if (role == null) throw new InvalidOperationException("Role not found");
        
        role.Name = dto.Name;
        role.Description = dto.Description;
        role.IsActive = dto.IsActive;
        
        // Update permissions
        role.RolePermissions.Clear();
        
        if (dto.PermissionCodes != null && dto.PermissionCodes.Any())
        {
            var permissions = await _permissionRepository.GetByCodesAsync(dto.PermissionCodes);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                });
            }
        }
        
        await _roleRepository.UpdateAsync(role);
        
        return await GetRoleByIdAsync(role.Id);
    }
    
    public async Task<bool> DeleteRoleAsync(long id)
    {
        return await _roleRepository.DeleteAsync(id);
    }
    
    public async Task<List<PermissionDto>> GetAllPermissionsAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();
        
        return permissions.Select(p => new PermissionDto
        {
            Id = p.Id,
            Code = p.Code,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category
        }).ToList();
    }
}