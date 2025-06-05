using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Web.BFF.Controllers;

public class BranchController : BaseApiController
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(long id)
    {
        var branch = await _branchService.GetBranchByIdAsync(id, CancellationToken.None);
        if (branch == null)
        {
            return NotFound();
        }
        return Ok(branch);
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches(BranchSearchRequestDto request)
    {
        var branches = await _branchService.GetBranchsAsync(request, CancellationToken.None);
        
        return Ok(branches);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateBranch(BranchCreationDto request)
    {
        var newCompany = await _branchService.CreateBranchAsync(request, CancellationToken.None);
        
        return Ok(newCompany);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBranch(BranchUpdateDto request)
    {
        var updatedCompany = await _branchService.UpdateBranchAsync(request, CancellationToken.None);
        
        return Ok(updatedCompany);
        
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveBranch(long id)
    {
        var isDeleted = await _branchService.DeleteBranchAsync(id, CancellationToken.None);
        
        if (!isDeleted)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
}