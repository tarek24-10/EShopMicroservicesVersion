namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession session, ILogger logger) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdQuery for ProductId: {ProductId}", query.ProductId);

            var productFromDb = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
            if (productFromDb == null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(productFromDb);
        }
    }
}
