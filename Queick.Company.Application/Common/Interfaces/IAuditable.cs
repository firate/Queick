namespace Queick.Company.Application.Common.Interfaces;

/// <summary>
/// Audit bilgilerini içeren entity'ler için arayüz
/// </summary>
public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
    DateTime? LastModifiedAt { get; set; }
    string LastModifiedBy { get; set; }
}