using Appointment.Entity;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data;

public class AppointmentReadOnlyContext : DbContext
{
    public AppointmentReadOnlyContext(DbContextOptions<AppointmentReadOnlyContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Location> Locations { get; set; }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("ReadOnly context!!");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("ReadOnly context!!");
    }
}
