namespace Queick.Company.Infrastructure;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}