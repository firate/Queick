using Microsoft.Extensions.DependencyInjection;
using Queick.Shared.Application.Interfaces;
using Queick.Shared.Infrastructure;

namespace Queick.Company.Infrastructure.Data;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
        
        return services;
    }
}