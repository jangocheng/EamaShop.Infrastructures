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
        public virtual async Task<ApiResponse<TResult>> SendAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter) where TResult : class
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
        /// <typeparam name="TParameter">请求的参数实体类型</typeparam>
        /// <typeparam name="TResult">响应的结果实体类型</typeparam>
        /// <param name="parameter">该请求的所携带的参数信息</param>
        /// <returns></returns>
        protected virtual Task<HttpClient> CreateHttpClientAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter) where TResult : class
        {
            _logger.LogTrace("获取默认的单例共享 HttpClient 对象");
            return Task.FromResult(HttpClient);
        }

        /// <summary>
        /// 从给定 <see cref="ApiParameter{TParameter, TResult}"/> 上创建该请求对应的
        ///  <see cref="HttpRequestMessage"/> 对象
        /// </summary>
        /// <typeparam name="TParameter">请求的参数实体类型</typeparam>
        /// <typeparam name="TResult">响应的结果实体类型</typeparam>
        /// <param name="parameter">该请求的所携带的参数信息</param>
        /// <returns></returns>
        protected virtual async Task<HttpRequestMessage> CreateRequestMessageAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter) where TResult : class
        {
            _logger.LogTrace("创建默认的请求消息参数上下文 HttpRequestMessage 对象");
            var method = parameter.Method;
            var uri = GenerateRequestUri(parameter, method);
            var result =
                new HttpRequestMessage(method, uri) { Content = await CreateHttpContentAsync(parameter, method, uri) };
            return result;
        }

        /// <summary>
        /// 默认直接采用请求参数的<see cref="ApiParameter{TParameter, TResult}.ApiUri"/>
        /// </summary>
        /// <typeparam name="TResult">响应的结果实体类型</typeparam>
        /// <typeparam name="TParameter">请求的参数实体类型</typeparam>
        /// <param name="parameter">请求信息</param>
        /// <param name="method">请求方法</param>
        /// <returns>请求的uri</returns>
        protected virtual Uri GenerateRequestUri<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter, HttpMethod method) where TResult : class
        {
            _logger.LogTrace("获取默认请求参数的Uri");
            return parameter.ApiUri;
        }

        /// <summary>
        /// 从给定的 <see cref="ApiParameter{TParameter, TResult}"/> 上创建与之对应的
        /// <see cref="HttpContent"/> 请求正文
        /// </summary>
        /// <typeparam name="TParameter">请求的参数实体类型</typeparam>
        /// <typeparam name="TResult">响应的结果实体类型</typeparam>
        /// <param name="parameter">请求信息</param>
        /// <param name="method">请求方法</param>
        /// <param name="uri">请求的uri</param>
        /// <returns>请求正文</returns>
        protected abstract Task<HttpContent> CreateHttpContentAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter, HttpMethod method, Uri uri)
             where TResult : class;

        /// <summary>
        /// 创建给定的 <see cref="ApiParameter{TParameter,TResult}"/> 的响应实体信息
        /// </summary>
        /// <typeparam name="TParameter">请求的参数类型</typeparam>
        /// <typeparam name="TResult">响应的实体</typeparam>
        /// <param name="parameter">发送的请求</param>
        /// <param name="response">第三方服务器返回的
        ///  <see cref="HttpResponseMessage"/> 信息</param>
        /// <returns><see cref="ApiResponse{TResult}"/> 类型的响应实体</returns>
        protected virtual async Task<ApiResponse<TResult>> CreateResponseAsync<TParameter, TResult>(
            ApiParameter<TParameter, TResult> parameter, HttpResponseMessage response) where TResult : class
        {

            var result = parameter.CreateResult(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType.ToString());

            return new DefaultApiResponse<TResult>(response.StatusCode,
                response.Headers.ToDictionary(x => x.Key, x => string.Join(" ", x.Value)), result);
        }

    }
}
