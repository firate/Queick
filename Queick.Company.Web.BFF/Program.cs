using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Queick.Company.Application;
using Queick.Company.Application.Interfaces;
using Queick.Company.Infrastructure;
using Queick.Company.Persistence;
using Queick.Company.Web.BFF.Middleware;
using Scalar.AspNetCore;

namespace Queick.Company.Web.BFF;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddInfrastructure();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        
        // Add services to the container.
       
        // Add Authentication
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"];
        var key = Encoding.ASCII.GetBytes(secretKey);
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        builder.Services.AddAuthorization();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // TODO: any kullanımı kısıtlanacak.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "Queick:"; // Prefix for all keys
        });
        
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var permissionRepository = scope.ServiceProvider.GetRequiredService<IPermissionRepository>();
            await permissionRepository.SeedPermissionsAsync();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

                  
        app.UseCors("AllowAll");
        
        app.UseMiddleware<JwtMiddleware>();
        
        app.UseAuthentication();
        app.UseAuthorization();

        

        await app.RunAsync();
    }
}