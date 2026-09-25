using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid OrderId);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var responce = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{responce.OrderId}", responce);
            })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new order")
            .WithDescription("Creates a new order with the provided details and returns the created order's ID.");
        }
    }
}
