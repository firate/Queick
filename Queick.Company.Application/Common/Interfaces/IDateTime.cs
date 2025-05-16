namespace Queick.Company.Application.Common.Interfaces;

/// <summary>
/// Tarih/zaman bilgilerini sağlayan arayüz
/// </summary>
public interface IDateTime
{
    DateTime Now { get; }
}