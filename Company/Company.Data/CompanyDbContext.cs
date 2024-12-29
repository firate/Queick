
using Domains;
using Microsoft.EntityFrameworkCore;

namespace CompanyData;

public class CompanyDbContext: DbContext
{
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Location> Locations { get; set; }
}