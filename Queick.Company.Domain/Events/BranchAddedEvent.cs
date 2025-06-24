using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;

public class BranchAddedEvent : DomainEvent
{
    public Guid CompanyId { get; }
    public Guid BranchId { get; }
    public string AddedBy { get; }

    public BranchAddedEvent(Guid companyId, Guid branchId, string addedBy)
    {
        CompanyId = companyId;
        BranchId = branchId;
        AddedBy = addedBy;
    }
}