using MassTransit;
using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Repositories;
using Queick.Company.Domain;

namespace Queick.Company.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // Queick.Company.Infrastructure
    
   
        
    public DbSet<CompanyDomain> Companies { get; set; }
        
    // IApplicationDbContext implementasyonu
    public async Task<T> GetByIdAsync<T>(long id, CancellationToken cancellationToken = default) where T : class, IEntity
    {
        return await Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }
        
    public async Task<List<T>> ListAsync<T>(CancellationToken cancellationToken = default) where T : class, IEntity
    {
        return await Set<T>().ToListAsync(cancellationToken);
    }
        
    public async Task<List<T>> ListAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }
        
    public async Task<T> FirstOrDefaultAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }
        
    public async Task<int> CountAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity
    {
        return await ApplySpecification(spec).CountAsync(cancellationToken);
    }
        
    public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IEntity
    {
        await Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }
        
    public void Update<T>(T entity) where T : class, IEntity
    {
        Set<T>().Update(entity);
    }
        
    public void Remove<T>(T entity) where T : class, IEntity
    {
        Set<T>().Remove(entity);
    }
        
    // Specification'ı uygulayan özel metot
    private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : class, IEntity
    {
        return SpecificationEvaluator<T>.GetQuery(Set<T>().AsQueryable(), spec);
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity configurations
            
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
// Specification'ları Entity Framework sorguları haline getiren sınıf