using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IApplicationMapper, ApplicationMapper>();
        services.AddScoped<ICompanyService, CompanyService>();
        
        return services;
    }
}
