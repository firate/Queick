namespace Queick.Company.Domain;

/// <summary>
/// Çalışan transfer kaydı entity sınıfı.
/// Bir çalışanın bir şirketten diğerine transferini takip eder.
/// </summary>
public class EmployeeTransferRecord : IEntity
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public long SourceCompanyId { get; set; }
    public long TargetCompanyId { get; set; }
    public DateTime TransferDate { get; set; }
    public string Notes { get; set; }
        
    // Domain logic/business rules
    public bool IsValidTransfer()
    {
        // Örnek bir business rule: Transfer tarihi geçmiş bir tarih olmalı
        return TransferDate <= DateTime.UtcNow;
    }
}