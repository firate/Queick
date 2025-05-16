using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Common.Interfaces;
using Queick.Company.Domain;
using IApplicationDbContext = Queick.Company.Application.Repositories.IApplicationDbContext;

namespace Queick.Company.Infrastructure.Data;

/// <summary>
/// IApplicationDbContext interface'inin Entity Framework Core ile implementasyonu.
/// Bu sınıf Infrastructure katmanında yer alır.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    private const int DefaultPageSize = 25;
    private const int MaxPageSize = 100;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime)
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    // Entity Framework DbSet'leri
    public DbSet<CompanyDomain> Companies { get; set; }
    public DbSet<EmployeeDomain> Employees { get; set; }
    public DbSet<EmployeeTransferRecord> EmployeeTransferRecords { get; set; }

    // IDatabaseFacade property'si için wrapper
    public IDatabaseFacade Database => new EfCoreDatabaseFacade(base.Database);

    #region IApplicationDbContext Implementation

    public IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity
    {
        return base.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(long id, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return (await (base.Set<TEntity>()).FindAsync(new object[] { id }, cancellationToken));
    }

    // Task<List<TEntity>> ListAsync<TEntity>(ISpecification<TEntity>? spec = null, int page = 1, int pageSize = 25,
    // CancellationToken cancellationToken = default) where TEntity : class, IEntity

    public async Task<List<TEntity>> ListAsync<TEntity>(ISpecification<TEntity>? spec = null, int page = 1,
        int pageSize = 25,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (spec is null)
        {
            return await ListAsync<TEntity>(page, pageSize, cancellationToken);
        }
        
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<TEntity>? FirstOrDefaultAsync<TEntity>(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var query = ApplySpecification(spec);
        return await query?.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountAsync<TEntity>(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return await ApplySpecification(spec).CountAsync(cancellationToken);
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var entry = await base.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var entry = base.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            base.Set<TEntity>().Attach(entity);
        }

        entry.State = EntityState.Modified;
        return Task.FromResult(entity);
    }

    public Task<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        base.Set<TEntity>().Remove(entity);
        return Task.FromResult(entity);
    }

    public async Task<List<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var entitiesList = entities.ToList();
        await base.Set<TEntity>().AddRangeAsync(entitiesList, cancellationToken);
        return entitiesList;
    }

    public Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var entitiesList = entities.ToList();
        foreach (var entity in entitiesList)
        {
            var entry = base.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                base.Set<TEntity>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        base.Set<TEntity>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Audit özellikleri doldur
        ApplyAuditProperties();

        // Domain event'leri topla
        var domainEvents = CollectDomainEvents();

        // Değişiklikleri kaydet
        var result = await base.SaveChangesAsync(cancellationToken);

        // Domain event'leri yayınla
        await PublishDomainEventsAsync(domainEvents, cancellationToken);

        return result;
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operations,
        CancellationToken cancellationToken = default)
    {
        // EF Core'un execution strategy'sini kullan
        var strategy = base.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await base.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await operations();
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        // EF Core transaction'ını IDbContextTransaction wrapper'ına sar
        var transaction = await base.Database.BeginTransactionAsync(cancellationToken);
        return new EfCoreDbContextTransaction(transaction);
    }

    #endregion

    #region Helper Methods

    private IQueryable<TEntity> ApplySpecification<TEntity>(ISpecification<TEntity> spec)
        where TEntity : class, IEntity
    {
        return SpecificationEvaluator<TEntity>.GetQuery(Set<TEntity>().AsQueryable(), spec);
    }

    private void ApplyAuditProperties()
    {
        var now = _dateTime.Now;
        var userId = _currentUserService?.UserId ?? "System";

        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = userId;
                    break;
            }
        }
    }
    
    private async Task<List<TEntity>> ListAsync<TEntity>(int page = 1, int pageSize = 25,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var safePageSize = pageSize switch
        {
            <= 0 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => pageSize
        };

        var safeSkip = page switch
        {
            <= 0 => 0,
            _ => safePageSize * (page - 1)
        };


        return await Set<TEntity>().Skip(safeSkip).Take(safePageSize).ToListAsync(cancellationToken);
    }

    private List<IDomainEvent> CollectDomainEvents()
    {
        // Eğer IDomainEvent ve IDomainEventEntity gibi arayüzleriniz varsa,
        // burada entity'lerden domain event'leri toplayabilirsiniz

        return new List<IDomainEvent>();
    }

    private async Task PublishDomainEventsAsync(List<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        // Eğer bir event bus (veya mediator) kullanıyorsanız,
        // burada domain event'leri yayınlayabilirsiniz

        await Task.CompletedTask;
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Domain entity'leri için konfigürasyonları uygula
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}