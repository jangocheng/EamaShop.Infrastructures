using EamaShop.Infrastructures;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add RabbitMQ <see cref="IEventBus"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, Action<RabbitMQEventBusOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Configure(configure);
            services.TryAddSingleton<IEventBus, RabbitMQEventBus>();
            services.TryAddSingleton<IConnectionFactory>(sp =>
            {
                var options= sp.GetRequiredService<IOptions<RabbitMQEventBusOptions>>();
                if (options.Value == null)
                {
                    throw new InvalidOperationException("option IOptions<RabbitMQEventBusOptions>.Value cannot be null");
                }
                return new ConnectionFactory()
                {
                    HostName = options.Value.Host,
                    Password = options.Value.Password,
                    UserName = options.Value.UserName
                };
            });

            services.TryAddSingleton<IRabbitMQPersistentConnection, RabbitMQPersistentConnection>();

            services.TryAddSingleton<IEventHandlerManager, EventBusHandlerManager>();
            return services;
        }
    }
}
