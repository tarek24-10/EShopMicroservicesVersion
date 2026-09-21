namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageUrl
        , decimal Price, List<string> Category) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) 
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating product with ID: {ProductId}", command.Id);

            var productfromDb = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productfromDb == null)
            {
                throw new ProductNotFoundException();
            }

            productfromDb.Update(command.Name, command.Description, command.ImageUrl
                , command.Price, command.Category);

            session.Update(productfromDb);

            await session.SaveChangesAsync(cancellationToken);
            
            return new UpdateProductResult(true);
        }
    }
}
