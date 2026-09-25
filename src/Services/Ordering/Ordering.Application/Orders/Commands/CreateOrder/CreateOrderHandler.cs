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
                Address.Of(orderDto.Shipping.FirstName, orderDto.Shipping.LastName, orderDto.Shipping.Email, orderDto.Shipping.Country, orderDto.Shipping.State, orderDto.Shipping.AddressLine, orderDto.Shipping.ZipCode),
                Address.Of(orderDto.Billing.FirstName, orderDto.Billing.LastName, orderDto.Billing.Email, orderDto.Billing.Country, orderDto.Billing.State, orderDto.Billing.AddressLine, orderDto.Billing.ZipCode),
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
