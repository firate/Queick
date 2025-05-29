using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Repositories;

public class CompanyRepository: BaseRepository<CompanyDomain>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<CompanyDomain?> GetCompanyWithBranchesAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsCompanyNameExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}