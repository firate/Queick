using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class CompanyRepository : BaseRepository<CompanyDomain>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<(List<CompanyDomain> Companies, int Count)> GetPagedAsync(
        string name,
        string description,
        int skip,
        int take,
        DateTimeOffset? createdFrom,
        DateTimeOffset? createdTo,
        bool onlyActives = true,
        bool onlyDeletedRecords = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Companies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            query = query.Where(c => c.Description.Contains(description));
        }

        if (createdFrom is not null)
        {
            query = query.Where(c => c.Created >= createdFrom);
        }

        if (createdTo is not null)
        {
            query = query.Where(c => c.Created <= createdTo);
        }

        query = query.Where(c => c.IsActive == onlyActives && c.IsDeleted == onlyDeletedRecords);

        var count = query.Count();

        if (count <= 0)
        {
            return ([], 0);
        }

        var companies = await query.Skip(skip).Take(take).ToListAsync(cancellationToken);

        return (companies, count);
    }

    public async Task<CompanyDomain?> GetCompanyByIdWithBranchesAsync(long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .Include(x => x.Branches)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> IsCompanyNameExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = _context.Companies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return await _context.Companies.AnyAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
    }
}