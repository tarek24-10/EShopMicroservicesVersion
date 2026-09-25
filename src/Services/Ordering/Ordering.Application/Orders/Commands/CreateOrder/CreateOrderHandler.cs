namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var order = Order.Create(
                OrderId.Of(orderDto.Id),
                CustomerId.Of(orderDto.CustomerId),
                OrderName.Of(orderDto.OrderName),
                Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.ZipCode),
                Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.ZipCode),
                Payment.Of(orderDto.Payment.PaymentMethod, orderDto.Payment.CardNumber, orderDto.Payment.CardName, orderDto.Payment.Expiration, orderDto.Payment.CVV)
            );

            foreach (var item in orderDto.Items)
            {
                order.AddOrderItem(ProductId.Of(item.ProductId), item.Price, item.Quantity);
            }

            return order;
        }
    }
}
