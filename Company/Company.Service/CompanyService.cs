using CompanyData;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class CompanyService : ICompanyService
{
    private readonly CompanyDbContext _companyDbContext;

    public CompanyService(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<Company> GetCompanyAsync(int id)
    {
        //null check
        if (id <= 0)
        { 
            ArgumentNullException.ThrowIfNull(id, nameof(id));
        }

        var company = await _companyDbContext.Companies.FindAsync(id);

        if ( company == null)
        {
            throw new ArgumentNullException(nameof(id), "Company not found");
        }
        return company;
    }

    // pagination
    public async Task<IEnumerable<Company>> GetCompaniesAsync(int page, int pageSize)
    {
        return await _companyDbContext.Companies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    
    
}