namespace Ordering.domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; private set; }
    }
}
