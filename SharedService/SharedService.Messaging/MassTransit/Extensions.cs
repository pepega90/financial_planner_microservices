using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SharedService.Messaging.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services,
        IConfiguration configuration, params Assembly[] assemblies)
        {
            services.AddMassTransit(config =>
            {
                foreach (var assembly in assemblies)
                {
                    config.AddConsumers(assembly);
                }
                config.UsingRabbitMq((ctx, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:UserName"]!);
                        host.Password(configuration["MessageBroker:Password"]!);
                    });
                    configurator.ConfigureEndpoints(ctx);
                });
            });
            return services;
        }
    }
}
