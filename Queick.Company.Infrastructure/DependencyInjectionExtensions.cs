using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Interfaces;

namespace Queick.Company.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
        
        return services;
    }
}