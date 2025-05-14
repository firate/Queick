namespace Queick.Company.Application;

public interface IRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T> GetAsync(long id);
    Task<T> UpdateAsync(long id, T entity);
    Task<bool> SoftDeleteAsync(long id);
    Task<IEnumerable<T>> GetAsync();
    
    
}