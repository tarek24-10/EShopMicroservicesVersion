namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand
    (string Name, string Description, string ImageUrl, decimal Price, List<string> Categories)
    : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Description, request.ImageUrl
                , request.Price, request.Categories);

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
