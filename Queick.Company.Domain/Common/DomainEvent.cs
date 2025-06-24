namespace Queick.Company.Domain.Common;

public abstract class DomainEvent: IDomainEvent
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset OccurredAt { get; protected set; } = DateTimeOffset.UtcNow;
}