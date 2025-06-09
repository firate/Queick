using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Common;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;   
    private readonly IDateTime _dateTime;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }
    
    public DbSet<CompanyDomain> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public override int SaveChanges()
    {
        AddAuditInfo();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddAuditInfo();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void AddAuditInfo()
    {
        var currentUserId =  _currentUserService.GetCurrentUserId();

        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditableEntity &&
                        (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in modifiedEntries)
        {
            var entity = (IAuditableEntity)entry.Entity;
            
            
            var now = _dateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entity.Created = now;
                entity.CreatedBy = currentUserId;
            }
            else
            {
                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(entity).Property(x => x.Created).IsModified = false;
            }

            entity.Updated = now;
            entity.UpdatedBy = currentUserId;
        }
    }
    
    
}