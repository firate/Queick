using System.Linq.Expressions;
using Queick.Shared.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    
    // Paging support
    Task<List<TEntity>> GetPagedAsync(int skip, int take, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    
    // Ordering support
    Task<List<TEntity>> GetPagedOrderedAsync<TKey>(int skip, int take, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    
    // CRUD operations
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(List<long> ids, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
}