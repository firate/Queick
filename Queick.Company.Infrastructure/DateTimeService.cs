using Queick.Company.Application.Interfaces;

namespace Queick.Company.Infrastructure;

/// <summary>
/// IDateTime interface'inin varsayılan implementasyonu.
/// Sistem saatini (UTC) kullanır.
/// </summary>
public class DateTimeService : IDateTime
{
    /// <summary>
    /// Mevcut UTC zamanını döndürür.
    /// </summary>
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}