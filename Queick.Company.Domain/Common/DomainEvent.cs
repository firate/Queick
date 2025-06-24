namespace Queick.Company.Domain.Common;

public abstract record DomainEvent(Guid Id, DateTimeOffset OccurredAt) : IDomainEvent
{
    protected DomainEvent() : this(Guid.NewGuid(), DateTimeOffset.UtcNow)
    {
    }
}