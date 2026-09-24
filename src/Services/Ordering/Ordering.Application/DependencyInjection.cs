using BuldingBlocks.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(currentAssembly);
                configuration.AddOpenBehavior(typeof(ValidationBeahviour<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(currentAssembly);

            return services;
        }
    }
}
