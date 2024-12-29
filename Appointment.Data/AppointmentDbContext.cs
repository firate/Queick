using Domains;
using Microsoft.EntityFrameworkCore;

namespace AppointmentData;
public class AppointmentDbContext:DbContext
{
    public AppointmentDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
    }

    public DbSet<Company> Companies { get; set; } 
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Location> Locations { get; set; }
}