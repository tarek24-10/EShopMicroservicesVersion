namespace Ordering.domain.ValueObjects
{
    public record Payment
    {
        public string PaymentMethod { get; } = default!;
        public string CardNumber { get; } = default!;
        public string CardName { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        protected Payment() { }
        private Payment(string paymentMethod, string cardNumber, string cardName, string expiration
            , string cvv)
        {
            PaymentMethod = paymentMethod;
            CardNumber = cardNumber;
            CardName = cardName;
            Expiration = expiration;
            CVV = cvv;
        }
        public static Payment Of(string paymentMethod, string cardNumber, string cardName,
            string expiration
            , string cvv)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            return new Payment(paymentMethod, cardNumber, cardName, expiration, cvv);
        }
    }
}
