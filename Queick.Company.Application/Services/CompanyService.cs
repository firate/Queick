using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Queick.Company.Application.Common.Exceptions;
using Queick.Company.Application.Common.Models;
using Queick.Company.Application.Common.Specification;
using Queick.Company.Application.Repositories;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IApplicationDbContext _dbContext;

    public CompanyService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompanyDto> GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        // Specification oluşturuluyor
        var spec = new CompanyByIdSpecification(id);

        // EF Core bağımlılığı olmadan sorgu yapılıyor
        var company = await _dbContext.FirstOrDefaultAsync(spec, cancellationToken);

        if (company == null)
            return null;

        // DTO dönüşümü
        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description
        };
    }

    public async Task<List<CompanyDto>> GetCompaniesAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var skip = (page - 1) * pageSize;
        var spec = new CompanyPaginatedSpecification(skip, pageSize);

        var companies = await _dbContext.ListAsync(spec, cancellationToken);

        return companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToList();
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto,
        CancellationToken cancellationToken = default)
    {
        if (companyDto is null)
        {
            throw new ArgumentNullException(nameof(companyDto));
        }
        
        var company = new CompanyDomain
        {
            Name = companyDto.Name,
            Description = companyDto?.Description ?? string.Empty
        };

        var result = await _dbContext.AddAsync(company, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        companyDto.Id = result.Id;
        return companyDto;
    }

    public async Task UpdateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default)
    {
        var spec = new CompanyByIdSpecification(companyDto.Id);
        var company = await _dbContext.FirstOrDefaultAsync(spec, cancellationToken);

        if (company == null)
            throw new NotFoundException($"Company with ID {companyDto.Id} not found.");

        company.Name = companyDto.Name;
        company.Description = companyDto.Description;

        _dbContext.Update(company);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCompanyAsync(long id, CancellationToken cancellationToken = default)
    {
        var spec = new CompanyByIdSpecification(id);
        var company = await _dbContext.FirstOrDefaultAsync(spec, cancellationToken);

        if (company == null)
            throw new NotFoundException($"Company with ID {id} not found.");

        _dbContext.Remove(company);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}