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
                    new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.ZipCode
                    ),
                    new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.ZipCode
                    ),
                    new PaymentDto(
                        order.Payment.PaymentMethod,
                        order.Payment.CardNumber,
                        order.Payment.CardName,
                        order.Payment.Expiration,
                        order.Payment.CVV
                    ),
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
