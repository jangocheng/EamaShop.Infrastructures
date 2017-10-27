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
        /// <typeparam name="TParameter">请求的参数实体类型</typeparam>
        /// <typeparam name="TResult">响应的结果实体类型</typeparam>
        /// <param name="parameter">该请求的所携带的参数信息</param>
        /// <returns></returns>
        Task<ApiResponse<TResult>> SendAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter)
            where TResult : class;
    }
}
