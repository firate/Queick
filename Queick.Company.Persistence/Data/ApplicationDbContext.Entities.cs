using Microsoft.EntityFrameworkCore;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Data
{
    public partial class ApplicationDbContext
    {
    
        // Entity Framework DbSet'leri
        public DbSet<CompanyDomain> Companies { get; set; }

    }
}