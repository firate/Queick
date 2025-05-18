namespace Queick.Shared.Application.Interfaces;

/// <summary>
/// Database Facade - EF Core bağımlılığını gizlemek için
/// </summary>
public interface IDatabaseFacade
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    IExecutionStrategy CreateExecutionStrategy();
}