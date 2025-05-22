using Microsoft.EntityFrameworkCore;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Data
{
    public partial class ApplicationDbContext
    {
    
        // Entity Framework DbSet'leri
        public DbSet<CompanyDomain> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CommunicationInfo> CommunicationInfos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}