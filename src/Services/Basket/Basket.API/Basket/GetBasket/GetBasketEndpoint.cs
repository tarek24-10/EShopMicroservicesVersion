namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string UserId);
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetBasketQuery(userName);
                var result = await sender.Send(new GetBasketQuery(userName), CancellationToken.None);
                var respose = result.Adapt<GetBasketResponse>();
                return Results.Ok(respose);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket by user name")
            .WithDescription("Get basket by user name");
        }
    }
}
