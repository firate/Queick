using Microsoft.AspNetCore.Mvc;
using Service;

namespace Company.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    
    private readonly IBranchService _branchService;
    
    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(long id)
    {
        var result = await _branchService.GetBranchAsync(id);
        
        if(result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequestModel request)
    {
        var branch = await _branchService.CreateBranchAsync(request.Name, request.CompanyId, request.Description);

        if (branch == null)
        {
            return NotFound();
        }
        
        return Created($"api/branch/{branch.Id}" ,branch);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(long id, [FromBody] UpdateBranchRequestModel request)
    {
        var result = await _branchService.UpdateBranchAsync(id, request.Name, request.Description);
        
        if(result == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
}