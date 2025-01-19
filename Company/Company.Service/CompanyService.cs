using CompanyData;
using Domains;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;

namespace Service;

public class CompanyService : ICompanyService
{
    private readonly CompanyDbContext _companyDbContext;

    public CompanyService(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<Company> GetCompanyByIdAsync(long id)
    {
        //null check
        if (id <= 0)
        { 
            ArgumentNullException.ThrowIfNull(id, nameof(id));
        }

        var company = await _companyDbContext.Companies.FindAsync(id);

        if (company == null)
        {
            throw new ArgumentNullException(nameof(id), "Company not found");
        }
        return company;
    }
    
    public async Task<PaginationResult<Company>> GetCompaniesAsync(string name, string description, int page, int pageSize, string sortBy, string sortDirection ="asc")
    {
        var query = _companyDbContext.Companies.AsQueryable().AsNoTracking();
        if(!string.IsNullOrEmpty(name))
            query = query.Where(x=> x.Name.Contains(name));
        
        if(!string.IsNullOrEmpty(description))
            query = query.Where(x=> x.Description.Contains(description));
        
        query =query.Skip((page - 1) * pageSize).Take(pageSize);
 
		// TODO: use sortBy and sortDirection to sort the query
		/*       
        if(sortDirection == "asc")
            query = query.OrderBy(x=>x.Id);
        else
            query = query.OrderByDescending(x=>x.Id);
		*/
        
        var companies = await query.ToListAsync();

        if (companies == null || companies?.Count == 0)
        {
            return new PaginationResult<Company>(new List<Company>(), 0, page, pageSize);
        }
        
        var totalCount = await _companyDbContext.Companies.CountAsync();
        
        return new PaginationResult<Company>(companies, totalCount, page, pageSize);
        
    }
    
    public async Task<Company> CreateCompanyAsync(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        }

        if (string.IsNullOrEmpty(description))
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(description, nameof(description));
        }

        var company = new Company
        {
            Name = name,
            Description = description
        };

        await _companyDbContext.Companies.AddAsync(company);
        await _companyDbContext.SaveChangesAsync();
        return company;
    }
   
    
}