namespace Queick.Company.Infrastructure;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}