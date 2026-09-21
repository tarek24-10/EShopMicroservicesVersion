namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.productId).NotEmpty().WithMessage("Product ID is required.");
        }
    }

    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) 
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting product with ID: {ProductId}", command.productId);

            session.Delete<Product>(command.productId);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
