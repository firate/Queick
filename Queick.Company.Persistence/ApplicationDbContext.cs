using Microsoft.EntityFrameworkCore;
using Queick.Company.Domain;

namespace Queick.Company.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<CompanyDomain> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; } 
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}