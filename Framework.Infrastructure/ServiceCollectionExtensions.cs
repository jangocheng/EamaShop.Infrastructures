using System;
using System.Collections.Generic;
using System.Text;
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
        /// 配置基础设施服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructures(this IServiceCollection service)
        {
            Checker.NotNull(service, nameof(service));
            service.TryAddSingleton<IPageEnumerableFactory, PageEnumerableFactory>();
            return service;
        }
    }
}
