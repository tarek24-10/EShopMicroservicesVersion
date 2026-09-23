using Discount.Grpc.Protos;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithName("Shopping Cart can not be null");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithName("User Name is required");
        }
    }
    internal class StoreBasketCommandHandler(IBasketRepository basketRepository
        , DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await ApplyDiscount(command.ShoppingCart, cancellationToken);
            await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }

        private async Task<ShoppingCart> ApplyDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            foreach (var item in shoppingCart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= (Decimal)coupon.Amount * item.Price;
            }
            return shoppingCart;
        }
    }
}
