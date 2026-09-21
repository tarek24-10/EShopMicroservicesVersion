namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid ProductId);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{productId:guid}", async (Guid productId, ISender sender) =>
            {
                var query = new GetProductByIdQuery(productId);
                var result = await sender.Send(query);
                var responce = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(responce);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get product by id")
            .WithDescription("Get product by id");
        }
    }
}
