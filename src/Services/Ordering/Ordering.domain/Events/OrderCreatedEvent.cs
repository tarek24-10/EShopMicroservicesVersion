namespace Ordering.domain.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvent;
}
