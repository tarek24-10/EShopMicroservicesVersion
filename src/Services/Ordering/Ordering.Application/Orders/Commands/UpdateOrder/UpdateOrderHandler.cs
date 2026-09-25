namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.Where(o => o.Id == orderId).Include(o => o.OrderItems).SingleOrDefaultAsync(cancellationToken);
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
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.ZipCode);

            var billingAddress = Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.ZipCode);

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
