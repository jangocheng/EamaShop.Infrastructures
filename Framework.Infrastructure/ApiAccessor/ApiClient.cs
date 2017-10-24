using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <inheritdoc />
    /// <summary>
    /// 为所有的对Http的WebApi接口请求客户端提供基础公共实现
    /// </summary>
    /// <typeparam name="TPlatform">该接口客户端的配置信息</typeparam>
    public abstract class ApiClient<TPlatform> : IApiClient<TPlatform> where TPlatform : IApiConfiguration
    {
        /// <summary>
        /// 获取默认的Http请求发送客户端
        /// </summary>
        public static HttpClient HttpClient { get; } = new HttpClient();

        private readonly ILogger _logger;
        /// <summary>
        /// 初始化 <see cref="ApiClient{TPlatform}"/> 类型的新实例
        /// </summary>
        protected ApiClient(TPlatform platform, ILoggerFactory factory)
        {
            Configuration = platform;
            _logger = factory.CreateLogger<ApiClient<TPlatform>>();
        }

        /// <inheritdoc />
        public TPlatform Configuration { get; }

        /// <inheritdoc />
        public virtual async Task<TResponse> SendAsync<TResponse>(ApiParameter<TResponse> parameter) where TResponse : ApiResponse
        {
            using (_logger.BeginRequest(parameter))
            {
                var client = await CreateHttpClientAsync(parameter);
                var request = await CreateRequestMessageAsync(parameter);
                var response = await client.SendAsync(request);
                _logger.LogResponse(Configuration.Name, response);
                var result = await CreateResponseAsync(parameter, response);
                return result;
            }
        }
        /// <summary>
        /// 创建发送指定请求的Api客户端
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected virtual Task<HttpClient> CreateHttpClientAsync<TResponse>(ApiParameter<TResponse> parameter) where TResponse : ApiResponse
        {
            _logger.LogTrace("获取默认的单例共享 HttpClient 对象");
            return Task.FromResult(HttpClient);
        }
        /// <summary>
        /// 从给定 <see cref="ApiParameter{TResponse}"/> 上创建该请求对应的
        ///  <see cref="HttpRequestMessage"/> 对象
        /// </summary>
        /// <typeparam name="TResponse">该请求的返回值</typeparam>
        /// <param name="parameter">该请求的所携带的参数信息</param>
        /// <returns></returns>
        protected virtual async Task<HttpRequestMessage> CreateRequestMessageAsync<TResponse>(ApiParameter<TResponse> parameter) where TResponse : ApiResponse
        {
            _logger.LogTrace("创建默认的请求消息参数上下文 HttpRequestMessage 对象");
            var method = parameter.GetMethod();
            var uri = GenerateRequestUri(parameter, method);
            var result =
                new HttpRequestMessage(method, uri) { Content = await CreateHttpContentAsync(parameter, method, uri) };
            return result;
        }

        /// <summary>
        /// 默认直接采用请求参数的<see cref="ApiParameter{TResponse}.GetApiUri"/>
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual Uri GenerateRequestUri<TResponse>(ApiParameter<TResponse> parameter, HttpMethod method) where TResponse : ApiResponse
        {
            _logger.LogTrace("获取默认请求参数的Uri");
            return parameter.GetApiUri();
        }
        /// <summary>
        /// 从给定的 <see cref="ApiParameter{TResponse}"/> 上创建与之对应的
        /// <see cref="HttpContent"/> 请求正文
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected abstract Task<HttpContent> CreateHttpContentAsync<TResponse>(ApiParameter<TResponse> parameter, HttpMethod method, Uri uri)
            where TResponse : ApiResponse;
        /// <summary>
        /// 创建给定的 <see cref="ApiParameter{TResponse}"/> 的响应实体信息
        /// </summary>
        /// <typeparam name="TResponse">响应的实体</typeparam>
        /// <param name="parameter">发送的请求</param>
        /// <param name="response">第三方服务器返回的
        ///  <see cref="HttpResponseMessage"/> 信息</param>
        /// <returns><typeparamref name="TResponse"/> 类型的响应实体</returns>
        protected virtual async Task<TResponse> CreateResponseAsync<TResponse>(ApiParameter<TResponse> parameter, HttpResponseMessage response)
            where TResponse : ApiResponse
        => parameter.CreateResponse(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType.ToString(), response.StatusCode, response.Headers.ToDictionary(x => x.Key, x => string.Join(" ", x.Value)));

    }
}
