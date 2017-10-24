using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Cache;
using Framework.Infrastructure.Configuration;
using Framework.Infrastructure.Converter;
using Framework.Infrastructure.DataPages;
using Framework.Infrastructure.MessageQueue;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 基础设施服务的容器扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 使用默认的配置，注册所有的基础设置服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructures(this IServiceCollection service)
        {
            service.AddInfrastructures(Configure);
            return service;
        }
        /// <summary>
        /// 使用指定的配置，配置基础设施服务
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructures(this IServiceCollection service,
            Action<InfrastructureOptions> configure)
        {
            Check.NotNull(configure, nameof(configure));
            var option = new InfrastructureOptions();
            configure(option);
            service.AddInfrastructures(option);
            return service;
        }
        /// <summary>
        /// 使用指定的配置，配置基础设施服务
        /// </summary>
        /// <param name="service"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructures(this IServiceCollection service, InfrastructureOptions options)
        {
            Check.NotNull(service, nameof(service));
            Check.NotNull(options, nameof(options));
            // redis
            service.TryAddSingleton<IAsyncCache, RedisAsyncCache>();
            service.Configure(options.RedisConfigure);
            // common
            service.TryAddSingleton<ISerializer, Serializer>();
            service.TryAddSingleton<IPageEnumerableFactory, PageEnumerableFactory>();
            // automapper
            service.TryAddSingleton<IMapper, AutoMapMapper>();
            //rabbit
            service.TryAddTransient(typeof(IProducer<>), typeof(RabbitMqProducer<>));
            service.TryAddTransient(typeof(IConsumer<>), typeof(RabbitMqConsumer<>));
            service.Configure(options.RabbitConfigure);

            return service;
        }

        internal static void Configure(InfrastructureOptions options)
        {
            options.RedisConfigure = ConfigureRedis;
            options.RabbitConfigure = ConfigureRabbit;
        }

        internal static void ConfigureRedis(RedisOptions options)
        {
            //options.IpAddress = "localhost";
            //options.Port = 6379;
            //options.Serializer = new Serializer();
        }

        internal static void ConfigureRabbit(RabbitMqOptions options)
        {
            //options.Encoding = Encoding.UTF8;
            //options.IpAddress = "localhost";
            //options.Password = "guest";
            //options.Port = 5672;
            //options.QueueSelector = (t) => t.FullName;
            //options.Serializer = new Serializer();
            //options.UserName = "guest";
        }
    }
}
