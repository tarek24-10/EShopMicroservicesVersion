using BuldingBlocks.CQRS;
namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new CreateOrderResult(request.Order.Id)
            {
                OrderId = Guid.NewGuid()
            });
        }
    }
}
