using BuldingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid OrderId);
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order name is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer ID is required");
            RuleFor(x => x.Order.Items).NotEmpty().WithMessage("Order items are required");
        }
    }
}
