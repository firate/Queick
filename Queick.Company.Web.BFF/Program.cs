using Queick.Company.Application;
using Queick.Company.Infrastructure;
using Queick.Company.Persistence;
using Scalar.AspNetCore;

namespace Queick.Company.Web.BFF;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddInfrastructure();
        builder.Services.AddApplicationServices();
        builder.Services.AddHttpContextAccessor();
        
        // Add services to the container.
       
        //builder.Services.AddAuthorization();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        //app.UseAuthorization();

        

        app.Run();
    }
}