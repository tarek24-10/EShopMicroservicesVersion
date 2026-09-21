namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        List<string> Category
    );

    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = new UpdateProductCommand(request.Id, request.Name, request.Description
                    , request.ImageUrl, request.Price, request.Category);

                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a product")
            .WithDescription("Update a product with the specified details.");
        }
    }
}
