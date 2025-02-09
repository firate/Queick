
using Company.Entity;
using Microsoft.EntityFrameworkCore;

namespace Company.Data;

public class CompanyDbContext: DbContext
{
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Location> Locations { get; set; }
}