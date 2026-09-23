using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class Extension
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            context.Database.MigrateAsync();

            return app;
        }
    }
}
