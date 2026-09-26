using BuldingBlocks.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var currentAssembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(currentAssembly);
                configuration.AddOpenBehavior(typeof(ValidationBeahviour<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(currentAssembly);

            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
