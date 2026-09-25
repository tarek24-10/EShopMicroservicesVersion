namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.OrderName))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            var orderDtos = ProjectToOrderDto(orders);

            return new GetOrdersByNameResult(orderDtos);
        }

        private List<OrderDto> ProjectToOrderDto(List<Order> orders)
        {
            List<OrderDto> result = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDto(
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    order.ShippingAddress.Adapt<AddressDto>(),
                    order.BillingAddress.Adapt<AddressDto>(),
                    order.Payment.Adapt<PaymentDto>(),
                    order.Status,
                    order.OrderItems.Select(oi => new OrderItemDto(
                        oi.Id.Value,
                        oi.ProductId.Value,
                        oi.Quantity,
                        oi.Price
                    )).ToList()
                );
                result.Add(orderDto);
            }
            return result;
        }
    }
}
