using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;

public class CompanyDeactivatedEvent : DomainEvent
{
    public Guid CompanyId { get; }
    public string DeactivatedBy { get; }
    public string Reason { get; }

    public CompanyDeactivatedEvent(Guid companyId, string deactivatedBy, string reason)
    {
        CompanyId = companyId;
        DeactivatedBy = deactivatedBy;
        Reason = reason;
    }
}