namespace Queick.Shared.Application.Interfaces;

/// <summary>
/// Tarih/zaman bilgilerini sağlayan arayüz
/// </summary>
public interface IDateTime
{
    DateTime Now { get; }
}