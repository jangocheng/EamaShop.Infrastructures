using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <summary>
    /// 表示对第三方平台Api的访问客户端
    /// </summary>
    public interface IApiClient<out TConfiguration> where TConfiguration:IApiConfiguration
    {
        /// <summary>
        /// 获取接口请求客户端的相关信息，包括客户端配置等信息
        /// </summary>
        TConfiguration Configuration { get; }
        /// <summary>
        /// 发送请求到第三方服务器
        /// </summary>
        /// <typeparam name="TResponse">请求返回的结果</typeparam>
        /// <param name="parameter">发送所携带的请求参数实体</param>
        /// <returns></returns>
        Task<TResponse> SendAsync<TResponse>(ApiParameter<TResponse> parameter) where TResponse : ApiResponse;
    }
}
