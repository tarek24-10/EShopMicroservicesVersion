namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid ProductId);
    public record DeleteProductResponce(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{productId:guid}", async (Guid productId, ISender sender) =>
            {
                var command = new DeleteProductCommand(productId);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponce>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes a product by its ID.")
            .WithDescription("Deletes a product by its ID. Returns a response indicating whether the deletion was successful.");
        }
    }
}
