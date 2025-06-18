namespace Queick.Company.Application.DTOs;

/// <summary>
/// Çalışan Data Transfer Object sınıfı
/// </summary>
public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
}