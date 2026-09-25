namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderwithNewValues(order, command.Order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderwithNewValues(Order order, OrderDto orderDto)
        {
            var shippingAddress = Address.Of(
                orderDto.Shipping.FirstName,
                orderDto.Shipping.LastName,
                orderDto.Shipping.Email,
                orderDto.Shipping.Country,
                orderDto.Shipping.State,
                orderDto.Shipping.AddressLine,
                orderDto.Shipping.ZipCode);

            var billingAddress = Address.Of(
                orderDto.Billing.FirstName,
                orderDto.Billing.LastName,
                orderDto.Billing.Email,
                orderDto.Billing.Country,
                orderDto.Billing.State,
                orderDto.Billing.AddressLine,
                orderDto.Billing.ZipCode);

            var payment = Payment.Of(
                orderDto.Payment.PaymentMethod,
                orderDto.Payment.CardNumber,
                orderDto.Payment.CardName,
                orderDto.Payment.Expiration,
                orderDto.Payment.CVV);

            order.Update(
                OrderName.Of(orderDto.OrderName),
                shippingAddress,
                billingAddress,
                payment,
                orderDto.Status
                );
        }
    }
}
