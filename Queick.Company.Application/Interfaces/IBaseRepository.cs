using System.Linq.Expressions;
using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task SoftDeleteAsync(long id, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(List<long> ids, CancellationToken cancellationToken = default);
}