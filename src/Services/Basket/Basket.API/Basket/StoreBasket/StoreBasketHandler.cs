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
    internal class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
