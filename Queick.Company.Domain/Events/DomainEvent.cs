using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.Events;

public abstract record DomainEvent(Guid Id, DateTimeOffset OccurredAt) : IDomainEvent
{
    protected DomainEvent() : this(Guid.NewGuid(), DateTimeOffset.UtcNow) { }
}