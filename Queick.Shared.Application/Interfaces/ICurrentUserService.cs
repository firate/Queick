namespace Queick.Company.Application.Common.Interfaces;

/// <summary>
/// Mevcut kullanıcı bilgilerini sağlayan arayüz
/// </summary>
public interface ICurrentUserService
{
    string UserId { get; }
}