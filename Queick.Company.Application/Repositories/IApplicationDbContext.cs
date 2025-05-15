using Queick.Company.Domain;

namespace Queick.Company.Application.Repositories;

public interface IApplicationDbContext
{
    // IQueryable yerine daha sade metodlar kullanılıyor
    Task<T> GetByIdAsync<T>(long id, CancellationToken cancellationToken = default) where T : class, IEntity;
    Task<List<T>> ListAsync<T>(CancellationToken cancellationToken = default) where T : class, IEntity;
    Task<List<T>> ListAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity;
    Task<T> FirstOrDefaultAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity;
    Task<int> CountAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class, IEntity;
        
    Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IEntity;
    void Update<T>(T entity) where T : class, IEntity;
    void Remove<T>(T entity) where T : class, IEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}