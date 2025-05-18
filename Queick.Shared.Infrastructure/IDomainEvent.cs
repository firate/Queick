namespace Queick.Shared.Infrastructure;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}