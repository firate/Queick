namespace Queick.Company.Application.Common.Interfaces;

/// <summary>
/// Transaction arayüzü - EF Core bağımlılığı olmadan
/// </summary>
public interface IDbContextTransaction : IAsyncDisposable, IDisposable
{
    Guid TransactionId { get; }
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}