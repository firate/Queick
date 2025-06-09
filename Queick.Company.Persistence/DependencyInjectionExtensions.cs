using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Interfaces;
using Queick.Company.Persistence.Repositories;

namespace Queick.Company.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RelationalDbConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(60);
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(15),
                        errorCodesToAdd: null);
                }
            ));

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        // after repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}