using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;

namespace Queick.Company.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<ITokenCacheService, TokenCacheService>();   
        return services;
    }
}