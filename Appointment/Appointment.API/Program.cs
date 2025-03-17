using AppointmentData;

using Microsoft.EntityFrameworkCore;
using AppointmentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppointmentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

builder.Services.AddDbContext<AppointmentReadOnlyContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
                npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10),null)
        ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    );

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAppointmentEntityService, AppointmentEntityService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

