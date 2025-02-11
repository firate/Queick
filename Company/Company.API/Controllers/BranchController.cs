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
    
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequestModel request)
    {
        var result = await _branchService.CreateBranchAsync(request.Name, request.Description);
        
        return Ok(result);
    }
    
}