using Queick.Company.Application.Common.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Repositories;

/// <summary>
/// Veritabanı işlemleri için temel arayüz.
/// UnitOfWork deseni uygulanmıştır - değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
/// </summary>
public interface IApplicationDbContext
{
    #region Sorgu Metotları

    IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity;

    Task<TEntity> GetByIdAsync<TEntity>(long id, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;
    
    /// <summary>
    /// Specification'a göre entity'leri listeleyen sorgu metodu.
    /// </summary>
    Task<List<TEntity>> ListAsync<TEntity>(IQueryModel<TEntity>? criterias = null, int page = 1, int pageSize = 25,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Specification'a göre ilk entity'i getiren sorgu metodu.
    /// </summary>
    Task<TEntity> FirstOrDefaultAsync<TEntity>(IQueryModel<TEntity> criterias,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Specification'a göre entity sayısını getiren sorgu metodu.
    /// </summary>
    Task<int> CountAsync<TEntity>(IQueryModel<TEntity> criterias, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    #endregion

    #region Modifikasyon Metotları

    /// <summary>
    /// Yeni entity ekler. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Entity'yi günceller. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Entity'yi siler. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Birden fazla entity ekler. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task<List<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Birden fazla entity'yi günceller. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    /// <summary>
    /// Birden fazla entity'yi siler. Değişiklikler SaveChangesAsync çağrılana kadar kaydedilmez.
    /// </summary>
    Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity;

    #endregion

    #region Transaction ve Kaydetme

    /// <summary>
    /// Tüm değişiklikleri veritabanına kaydeder.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Bir transaction içinde özel bir işlem gerçekleştirir.
    /// Bu metot, birden fazla veritabanı işleminin atomik olarak gerçekleştirilmesini sağlar.
    /// </summary>
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operations, CancellationToken cancellationToken = default);

    /// <summary>
    /// Yeni bir transaction başlatır.
    /// Bu metot, daha büyük bir scope içinde transaction yönetimi yapmak isteyenler içindir.
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// DbContext'in temel Database özelliğine erişim sağlar.
    /// </summary>
    IDatabaseFacade Database { get; }

    #endregion
}