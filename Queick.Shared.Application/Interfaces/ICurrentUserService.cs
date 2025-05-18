namespace Queick.Shared.Application.Interfaces;

/// <summary>
/// Mevcut kullanıcı bilgilerini sağlayan arayüz
/// </summary>
public interface ICurrentUserService
{
    string UserId { get; }
}