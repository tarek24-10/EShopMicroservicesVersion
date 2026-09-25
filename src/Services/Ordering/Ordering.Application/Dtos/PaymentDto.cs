namespace Ordering.Application.Dtos
{
    public record PaymentDto(
         string PaymentMethod,
         string CardName,
         string CardNumber,
         string Expiration,
         string CVV
     );
}
