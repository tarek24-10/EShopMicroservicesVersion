using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration event handled: {0}", context.Message.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        private object MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress
                , message.Country, message.State, message.AddressLine, message.ZipCode);

            var payementDto = new PaymentDto(message.PaymentMethod, message.CardName, message.CardNumber,
                message.Expiration, message.CVV);

            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName
                , addressDto, addressDto, payementDto, OrderStatus.Pending,
                [
                    new OrderItemDto(orderId, Guid.NewGuid(), 2, 500),
                    new OrderItemDto(orderId, Guid.NewGuid(), 1, 400)
                ]
                );
            return new CreateOrderCommand(orderDto);
        }
    }
}
