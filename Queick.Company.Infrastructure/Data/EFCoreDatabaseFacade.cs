using Microsoft.EntityFrameworkCore.Infrastructure;
using Queick.Company.Application.Common.Interfaces;

namespace Queick.Company.Infrastructure.Data;

/// <summary>
/// EF Core DatabaseFacade için wrapper sınıfı
/// </summary>
public class EfCoreDatabaseFacade : IDatabaseFacade
{
    private readonly DatabaseFacade _database;
        
    public EfCoreDatabaseFacade(DatabaseFacade database)
    {
        _database = database;
    }
        
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _database.BeginTransactionAsync(cancellationToken);
        return new EfCoreDbContextTransaction(transaction);
    }
        
    public IExecutionStrategy CreateExecutionStrategy()
    {
        // Burada wrapper dönmüyoruz, interface niteliğini 
        // sadece marker olarak kullanıyoruz ve uygulamada 
        // ExecuteInTransactionAsync metodunda doğrudan EF Core'u kullanıyoruz
        return null;
    }
}