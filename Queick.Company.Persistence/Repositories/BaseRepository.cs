using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Shared.Domain;

namespace Queick.Company.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }



    // public virtual async Task<List<TEntity>> GetPagedAsync(int skip, int take,
    //     Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    // {
    //     var query = _dbSet.AsQueryable();
    //
    //     if (predicate != null)
    //     {
    //         query = query.Where(predicate);
    //     }
    //
    //     return await query.Skip(skip).Take(take).ToListAsync(cancellationToken);
    // }

    // public virtual async Task<List<TEntity>> GetPagedOrderedAsync<TKey>(int skip, int take,
    //     Expression<Func<TEntity, TKey>> orderBy, bool ascending = true,
    //     Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    // {
    //     var query = _dbSet.AsQueryable();
    //
    //     if (predicate != null)
    //         query = query.Where(predicate);
    //
    //     query = ascending
    //         ? query.OrderBy(orderBy)
    //         : query.OrderByDescending(orderBy);
    //
    //     return await query.Skip(skip).Take(take).ToListAsync(cancellationToken);
    // }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Created = DateTimeOffset.UtcNow;
        var result = await _dbSet.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public virtual async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        entities.ForEach(e => e.Created = now);

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Updated = DateTimeOffset.UtcNow;
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        entities.ForEach(e => e.Updated = now);

        _dbSet.UpdateRange(entities);
        return Task.FromResult(entities);
    }

    public virtual async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return true;
    }


    public virtual async Task<bool> DeleteRangeAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        if (entities.Count <= 0)
            return false;

        _dbSet.RemoveRange(entities);
        return true;
    }
}