using Queick.Company.Application.Common.Interfaces;

namespace Queick.Company.Infrastructure.Data;

/// <summary>
/// EF Core IDbContextTransaction için wrapper sınıfı
/// </summary>
public class EfCoreDbContextTransaction : IDbContextTransaction
{
    private readonly Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;
        
    public EfCoreDbContextTransaction(Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }
        
    public Guid TransactionId => _transaction.TransactionId;
        
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }
        
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }
        
    public void Dispose()
    {
        _transaction.Dispose();
    }
        
    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
    }
}