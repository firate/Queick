using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Interfaces;
using Queick.Company.Persistence.Repositories;

namespace Queick.Company.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        
        return services;
    }
}