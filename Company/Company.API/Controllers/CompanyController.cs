using Microsoft.AspNetCore.Mvc;
using AppointmentService;

namespace Company.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequestModel request)
    {
        var result = await _companyService.CreateCompanyAsync(request.Name, request.Description);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(long id, [FromBody] UpdateCompanyRequestModel request)
    {
        var result = await _companyService.UpdateCompanyAsync(id, request.Name, request.Description);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(long id)
    {
        var result = await _companyService.GetCompanyByIdAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies(string name, string description, int page, int pageSize,
        string sortBy, string sortDirection)
    {
        var result = await _companyService.GetCompaniesAsync(name, description, page, pageSize, sortBy, sortDirection);

        return Ok(result);
    }
}