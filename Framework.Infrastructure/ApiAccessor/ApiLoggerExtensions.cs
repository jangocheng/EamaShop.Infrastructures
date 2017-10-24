using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <summary>
    /// Api客户端的日志记录扩展
    /// </summary>
    internal static class ApiLoggerExtensions
    {
        /// <summary>
        /// 开始请求的日志生命周期
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="parameter"></param>
        public static IDisposable BeginRequest<TResponse>(this ILogger logger, ApiParameter<TResponse> parameter) where TResponse : ApiResponse
        {
            return logger.BeginScope(parameter);
        }
        /// <summary>
        /// 记录该请求信息
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="clientName"></param>
        /// <param name="httpResponse"></param>
        public static void LogResponse(this ILogger logger, string clientName, HttpResponseMessage httpResponse)
        {
            logger.LogInformation(Resources.ApiClientResponseDescription(clientName, httpResponse));
        }
    }
}
