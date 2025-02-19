using Company.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace CompanyAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        
        // TODO: temporarily commented out
        //builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        builder.Services.AddDbContext<CompanyDbContext>(options =>
            options.UseNpgsql(connectionString, 
                npgsqlOptions => npgsqlOptions.CommandTimeout(60)
            ));
        
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<IBranchService, BranchService>();

        var rabbitMqHost = builder.Configuration["RabbitMq:Host"];
        var rabbitMqUsername = builder.Configuration["RabbitMq:Username"];
        var rabbitMqPassword = builder.Configuration["RabbitMq:Password"];
        
        builder.Services.AddMassTransit(x =>
        {
         
            x.AddEntityFrameworkOutbox<CompanyDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(10);

                o.UsePostgres();
                o.UseBusOutbox();
            });
            
            x.UsingRabbitMq((context, cfg) =>
            {
                
                cfg.Host(rabbitMqHost, "/", h =>
                {
                    h.Username(rabbitMqUsername);
                    h.Password(rabbitMqPassword);
                });
                cfg.ConfigureEndpoints(context);
                
            });
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        // TODO: temporarily commented out 
        //app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}