using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Web.BFF.Controllers.Base;

namespace Queick.Company.Web.BFF.Controllers;

public class CompanyController : BaseApiController
{
    private readonly ICompanyService _companyService;
    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id, CancellationToken.None);

        if (company is null)
        {
            return NotFound();
        }
        
        return Ok(company);
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies(CompanySearchRequestDto request)
    {
        var companies = await _companyService.GetCompaniesAsync(request, CancellationToken.None);
        
        return Ok(companies);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyCreationDto request)
    {
        var newCompany = await _companyService.CreateCompanyAsync(request, CancellationToken.None);

        return Ok(newCompany);
        
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCompany(CompanyUpdateDto request)
    {
        var updatedCompany = await _companyService.UpdateCompanyAsync(request, CancellationToken.None);

        return Ok(updatedCompany);
        
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveCompany(Guid id)
    {
        var isDeleted = await _companyService.DeleteCompanyAsync(id, CancellationToken.None);

        if (!isDeleted)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
    
    
}