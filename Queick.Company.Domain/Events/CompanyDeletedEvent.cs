using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;

public class CompanyDeletedEvent : DomainEvent
{
    public Guid CompanyId { get; }
    public string DeletedBy { get; }
    public string Reason { get; }

    public CompanyDeletedEvent(Guid companyId, string deletedBy, string reason)
    {
        CompanyId = companyId;
        DeletedBy = deletedBy;
        Reason = reason;
    }
}