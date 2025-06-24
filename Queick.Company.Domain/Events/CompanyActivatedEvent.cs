using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;
public class CompanyActivatedEvent : DomainEvent
{
    public Guid CompanyId { get; }
    public string ActivatedBy { get; }

    public CompanyActivatedEvent(Guid companyId, string activatedBy)
    {
        CompanyId = companyId;
        ActivatedBy = activatedBy;
    }
}

