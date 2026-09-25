namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ProjectToOrderDto(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto(
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    order.ShippingAddress.Adapt<AddressDto>(),
                    order.BillingAddress.Adapt<AddressDto>(),
                    order.Payment.Adapt<PaymentDto>(),
                    order.Status,
                    order.OrderItems.Select(oi => new OrderItemDto(
                        oi.Id.Value,
                        oi.ProductId.Value,
                        oi.Quantity,
                        oi.Price
                    )).ToList()
                ));
        }
    }
}
