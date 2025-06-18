using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Domain.Common;

namespace Queick.Company.Persistence.Repositories.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity: Entity
{
    protected readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public virtual async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        return Task.FromResult(entities);
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            // TODO: custom exception kullanalım.
            throw new Exception();
        }

        if (entity is ISoftDeleteEntity softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            softDeleteEntity.DeletedAt = DateTimeOffset.UtcNow;
        }
        
        _dbSet.Update(entity);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new Exception();
        }
 
        _dbSet.Remove(entity);
    }
    
    public virtual async Task<bool> DeleteRangeAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        if (entities.Count <= 0)
        {
            return false;
        }

        _dbSet.RemoveRange(entities);
        return true;
    }
}