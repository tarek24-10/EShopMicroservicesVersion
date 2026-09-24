namespace Ordering.domain.Events
{
    public record OrderUpdatedEvent(Order Order) : IDomainEvent;
}
