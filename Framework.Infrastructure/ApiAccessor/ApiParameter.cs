using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <summary>
    /// 表示访问第三方接口所带的请求参数
    /// </summary>
    public abstract class ApiParameter<TResponse> where TResponse : ApiResponse
    {
        /// <summary>
        /// 获取请求的Api的Uri地址
        /// </summary>
        /// <returns></returns>
        public abstract Uri GetApiUri();
        /// <summary>
        /// 获取请求方式
        /// </summary>
        /// <returns></returns>
        public abstract HttpMethod GetMethod();

        /// <summary>
        /// 从响应的字符串正文中创建与该请求相关联的响应实体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public abstract TResponse CreateResponse(string content,string contentType,HttpStatusCode code,IDictionary<string,string> headers);
    }

}
