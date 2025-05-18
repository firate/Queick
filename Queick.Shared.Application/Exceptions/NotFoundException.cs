namespace Queick.Company.Application.Common.Exceptions;

/// <summary>
/// Bulunamayan entity için özel istisna sınıfı
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}