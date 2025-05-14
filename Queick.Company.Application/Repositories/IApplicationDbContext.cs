namespace Queick.Company.Application.Repositories;

public interface IApplicationDbContext
{
    IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity;
    
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
    
    void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
    
    void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}