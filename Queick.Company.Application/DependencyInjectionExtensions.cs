using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        
        return services;
    }
}
