using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<List<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        var roleDtos = new List<RoleDto>();

        foreach (var role in roles)
        {
            var roleWithPermissions = await _unitOfWork.Roles.GetRoleWithPermissionsAsync(role.Id);
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

    public async Task<RoleDto> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await _unitOfWork.Roles.GetRoleWithPermissionsAsync(id);
        if (role == null) throw new InvalidOperationException("Role not found");

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

    public async Task<RoleDto> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken = default)
    {
        var role = new Role
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true
        };

        await _unitOfWork.Roles.AddAsync(role);

        // Add permissions
        if (dto.PermissionCodes != null && dto.PermissionCodes.Any())
        {
            var permissions = await _unitOfWork.Permissions.GetByCodesAsync(dto.PermissionCodes);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                });
            }

            await _unitOfWork.Roles.UpdateAsync(role);
        }

        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!isSaved)
        {
            throw new Exception("Role not created!");
        }

        return await GetRoleByIdAsync(role.Id, cancellationToken);
    }

    public async Task<RoleDto> UpdateRoleAsync(UpdateRoleDto dto, CancellationToken cancellationToken = default)
    {
        var role = await _unitOfWork.Roles.GetRoleWithPermissionsAsync(dto.Id);
        if (role == null) throw new InvalidOperationException("Role not found");

        role.Name = dto.Name;
        role.Description = dto.Description;
        role.IsActive = dto.IsActive;

        // Update permissions
        role.RolePermissions.Clear();

        if (dto.PermissionCodes != null && dto.PermissionCodes.Any())
        {
            var permissions = await _unitOfWork.Permissions.GetByCodesAsync(dto.PermissionCodes);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                });
            }
        }

        await _unitOfWork.Roles.UpdateAsync(role);

        return await GetRoleByIdAsync(role.Id);
    }

    public async Task<bool> DeleteRoleAsync(Guid id,  CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Roles.DeleteAsync(id,  cancellationToken);
        
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<PermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var permissions = await _unitOfWork.Permissions.GetAllAsync();

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