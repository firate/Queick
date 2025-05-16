using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Common.Interfaces;

namespace Queick.Company.Infrastructure.Data;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
        
        return services;
    }
}