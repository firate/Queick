using Microsoft.AspNetCore.Mvc;
using Queick.Company.Application.Common.Models;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Web.BFF.Controllers;

public class CompanyController : BaseApiController
{
    private readonly ICompanyService _companyService;
    //private readonly Mapper _mapper;

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
    
    
}