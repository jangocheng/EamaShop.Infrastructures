using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Converter;
using Framework.Infrastructure.MessageQueue;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Framework.Infrastructure;
using Framework.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection
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
            service.AddInfrastructures((opt) =>
            {

            });
            return service;
        }
        /// <summary>
        /// 使用指定的配置，配置基础设施服务
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructures(this IServiceCollection service,
            Action<RabbitMqOptions> configure)
        {
            Checker.NotNull(configure, nameof(configure));
            Checker.NotNull(service, nameof(service));
            service.Configure(configure);
            // common
            service.TryAddSingleton<ISerializer, Serializer>();
            service.TryAddSingleton<IPageEnumerableFactory, PageEnumerableFactory>();
            // automapper
            service.TryAddSingleton<IMapper, AutoMapMapper>();
            //rabbit
            service.TryAddTransient(typeof(IProducer<>), typeof(RabbitMqProducer<>));
            service.TryAddTransient(typeof(IConsumer<>), typeof(RabbitMqConsumer<>));
            service.TryAddSingleton(typeof(IObservable<>), typeof(RabbitObservable<>));
            return service;
        }
    }
}
