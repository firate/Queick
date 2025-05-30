using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.Common.Models;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Web.BFF.Controllers;

public class CompanyController : BaseApiController
{
    private readonly ICompanyService _companyService;
    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }


    [HttpGet]
    public async Task<IActionResult> GetCompanies(CompanySearchRequestDto searchRequest)
    {
        var companies = await _companyService.GetCompaniesAsync(searchRequest, CancellationToken.None);
        
        return Ok(companies);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyCreationDto dto)
    {
        var newCompany = await _companyService.CreateCompanyAsync(dto, CancellationToken.None);

        return Ok(newCompany);
        
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCompany(CompanyUpdateDto dto)
    {
        var updatedCompany = await _companyService.UpdateCompanyAsync(dto, CancellationToken.None);

        return Ok(updatedCompany);
        
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveCompany(long id)
    {
        var isDeleted = await _companyService.DeleteCompanyAsync(id, CancellationToken.None);

        if (!isDeleted)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
    
    
}