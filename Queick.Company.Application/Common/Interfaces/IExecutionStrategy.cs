namespace Queick.Company.Application.Common.Interfaces;

/// <summary>
/// Execution Strategy arayüzü - EF Core bağımlılığını gizlemek için
/// </summary>
public interface IExecutionStrategy
{
    Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken = default);
}