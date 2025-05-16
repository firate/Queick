namespace Queick.Company.Application.DTOs;

/// <summary>
/// Çalışan Data Transfer Object sınıfı
/// </summary>
public class EmployeeDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long CompanyId { get; set; }
    public string CompanyName { get; set; }
}