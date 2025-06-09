using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.Authorization;
using Queick.Company.Application.DTOs.Auth;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Web.BFF.Attributes;

namespace Queick.Company.Web.BFF.Controllers;

public class RoleController : BaseApiController
{
    private readonly IRoleService _roleService;
    
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet]
    [RequirePermission(Permissions.Role.Read)]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }
    
    [HttpGet("{id}")]
    [RequirePermission(Permissions.Role.Read)]
    public async Task<IActionResult> GetRoleById(long id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }
    
    [HttpGet("permissions")]
    [RequirePermission(Permissions.Role.Read)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _roleService.GetAllPermissionsAsync();
        return Ok(permissions);
    }
    
    [HttpPost]
    [RequirePermission(Permissions.Role.Write)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
    {
        var role = await _roleService.CreateRoleAsync(dto);
        return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
    }
    
    [HttpPut("{id}")]
    [RequirePermission(Permissions.Role.Write)]
    public async Task<IActionResult> UpdateRole(long id, [FromBody] UpdateRoleDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch");
        }
        
        try
        {
            var role = await _roleService.UpdateRoleAsync(dto);
            return Ok(role);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [RequirePermission(Permissions.Role.Delete)]
    public async Task<IActionResult> DeleteRole(long id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}