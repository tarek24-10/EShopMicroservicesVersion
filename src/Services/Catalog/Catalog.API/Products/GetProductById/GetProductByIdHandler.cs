namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var productFromDb = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
            if (productFromDb == null)
            {
                throw new ProductNotFoundException(query.ProductId);
            }
            return new GetProductByIdResult(productFromDb);
        }
    }
}
