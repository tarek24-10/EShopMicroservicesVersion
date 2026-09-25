namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            var count = await context.Orders.LongCountAsync(cancellationToken);

            var orderDtos = orders.ProjectToOrderDto();
            var result = new PaginatedResult<OrderDto>(pageIndex, pageSize, count, orderDtos);
            return new GetOrdersResult(result);
        }
    }
}
