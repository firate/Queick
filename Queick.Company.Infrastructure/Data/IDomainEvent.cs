namespace Queick.Company.Infrastructure.Data;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}