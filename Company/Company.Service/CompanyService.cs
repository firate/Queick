using Company.Data;
using Company.Entity;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Linq;
using EventsLibrary;
using MassTransit;

namespace Service;

public class CompanyService : ICompanyService
{
    private readonly CompanyDbContext _companyDbContext;
    private readonly IPublishEndpoint _publishEndpoint;
    public CompanyService(CompanyDbContext companyDbContext, IPublishEndpoint publishEndpoint)
    {
        _companyDbContext = companyDbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CompanyEntity> GetCompanyByIdAsync(long id)
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
    
    public async Task<PaginationResult<CompanyEntity>> GetCompaniesAsync(string name, string description, int page, int pageSize, string sortBy, string sortDirection ="asc")
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

        if (companies?.Count == 0)
        {
            return new PaginationResult<CompanyEntity>(new List<CompanyEntity>(), 0, page, pageSize);
        }
        
        var totalCount = await _companyDbContext.Companies.CountAsync();
        
        return new PaginationResult<CompanyEntity>(companies, totalCount, page, pageSize);
        
    }

    public async Task<CompanyEntity> UpdateCompanyAsync(long id, string name, string description)
    {
        var company = await _companyDbContext.Companies.FindAsync(id);
        
        if (company == null)
        {
            throw new ArgumentNullException(nameof(id), "Company not found");
        }
        
        if (!string.IsNullOrEmpty(name))
        {
            company.Name = name;
        }
        
        if (!string.IsNullOrEmpty(description))
        {
            company.Description = description;
        }
        
        var changed = await _companyDbContext.SaveChangesAsync();

        if (changed <= 0)
        {
            //TODO: log the error
            // create new Exception type
            throw new ArgumentNullException(nameof(id), "Company not updated");
        }
        
        return company;
    }
    
    public async Task<bool> DeleteCompanyAsync(long id)
    {
        var company = await _companyDbContext.Companies.FindAsync(id);
        
        if (company == null)
        {
            throw new ArgumentNullException(nameof(id), "Company not found");
        }
        
        company.IsDeleted = true;
        
        var changed = await _companyDbContext.SaveChangesAsync();

        if (changed <= 0)
        {
            //TODO: log
            return false;
            //throw new ArgumentNullException(nameof(id), "Company not deleted");
        }
        
        _publishEndpoint.Publish<CompanyDeletedEvent>(
            new CompanyDeletedEvent
            {
                Id = company.Id
            }
        );
        
        return true;
    }

    public async Task<CompanyEntity> CreateCompanyAsync(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        }

        if (string.IsNullOrEmpty(description))
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(description, nameof(description));
        }

        var company = new CompanyEntity
        {
            Name = name,
            Description = description
        };

        await _companyDbContext.Companies.AddAsync(company);
        var created =  await _companyDbContext.SaveChangesAsync();
        
        // TODO: check if the company is created successfully

        if (created > 0)
        {
            var createdEvent = new CompanyCreatedEvent()
            {
                Id =company.Id,
                Name = company.Name,
                Description = company.Description
            };
            
            await _publishEndpoint.Publish<CompanyCreatedEvent>(createdEvent);
        }
        
        return company;
    }
   
    
}