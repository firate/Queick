using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;

public class EntitySoftDeletedEvent : DomainEvent
{
    public string DeletedBy { get; }
    public string Reason { get; }

    public EntitySoftDeletedEvent( string deletedBy, string reason)
    {
        
        DeletedBy = deletedBy;
        Reason = reason;
    }
}