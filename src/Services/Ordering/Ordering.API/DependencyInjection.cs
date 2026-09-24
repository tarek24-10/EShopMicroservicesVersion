using BuldingBlocks.Exceptions.Handler;
using Carter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(Program).Assembly;

            services.AddCarter(null, configurator =>
            {
                var moduleTypes = currentAssembly
                    .GetTypes()
                    .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
                    .ToArray();

                configurator.WithModules(moduleTypes);
            });

            services.AddExceptionHandler<CustomExceptionHandler>();

            //services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
