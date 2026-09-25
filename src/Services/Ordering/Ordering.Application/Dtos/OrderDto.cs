using Ordering.domain.Enums;

namespace Ordering.Application.Dtos
{
    public record OrderDto(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto Shipping,
        AddressDto Billing,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> Items
    );
}
