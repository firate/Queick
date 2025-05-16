using Queick.Company.Application.Common.Interfaces;

namespace Queick.Company.Infrastructure.Data;

/// <summary>
/// IDateTime interface'inin varsayılan implementasyonu.
/// Sistem saatini (UTC) kullanır.
/// </summary>
public class DateTimeService : IDateTime
{
    /// <summary>
    /// Mevcut UTC zamanını döndürür.
    /// </summary>
    public DateTime Now => DateTime.UtcNow;
}