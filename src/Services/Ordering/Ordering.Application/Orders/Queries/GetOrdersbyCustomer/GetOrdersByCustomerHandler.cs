using Ordering.Application.Orders.Queries.GetOrdersByName;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Application.Orders.Queries.GetOrdersbyCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            var orderDtos = orders.ProjectToOrderDto();
            return new GetOrdersByCustomerResult(orderDtos);
        }
    }
}
