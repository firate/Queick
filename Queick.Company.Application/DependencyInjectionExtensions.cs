using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queick.Company.Application.Common;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddTransient<IApplicationMapper, ApplicationMapper>();

        services.AddScoped<IJwtService, JwtService>();


        // Auth provider seçimi (opsiyonel - ileride kullanmak için)
        var authProvider = configuration["Authentication:Provider"] ?? "JWT";

        switch (authProvider.ToUpper())
        {
            case "JWT":
                services.AddScoped<IAuthService, AuthService>();
                break;

            case "KEYCLOAK":
                // İleride eklenecek
                // services.AddScoped<IAuthService, KeycloakAuthService>();
                throw new NotImplementedException("Keycloak provider not implemented yet");

            default:
                services.AddScoped<IAuthService, AuthService>();
                break;
        }

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IBranchService, BranchService>();

        return services;
    }
}