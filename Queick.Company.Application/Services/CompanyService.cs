using Queick.Company.Application.Common.Models;
using Queick.Company.Application.Repositories;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CompanyService(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<CompanyDto> GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}