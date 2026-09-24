using MediatR;

namespace Ordering.domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid Id => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName;
    }
}
