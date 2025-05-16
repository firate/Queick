namespace Queick.Company.Application.DTOs;

/// <summary>
/// Transfer sonucu Data Transfer Object sınıfı
/// </summary>
public class TransferResultDto
{
    public bool Success { get; set; }
    public long EmployeeId { get; set; }
    public long SourceCompanyId { get; set; }
    public long TargetCompanyId { get; set; }
    public long TransferRecordId { get; set; }
}