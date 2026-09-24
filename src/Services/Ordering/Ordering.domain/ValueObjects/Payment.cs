namespace Ordering.domain.ValueObjects
{
    public record Payment
    {
        public string PaymentMethod { get; } = default!;
        public string CardNumber { get; } = default!;
        public string CardName { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
    }
}
