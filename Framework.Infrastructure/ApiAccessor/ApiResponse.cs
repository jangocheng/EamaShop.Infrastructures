using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <summary>
    /// 表示第三方接口请求返回的正文实体公共基类
    /// </summary>
    public abstract class ApiResponse
    {
        
        /// <summary>
        /// 使用指定的状态码初始化ApiResponse的新实例
        /// </summary>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        protected ApiResponse(HttpStatusCode code, IDictionary<string, string> headers)
        {
            StatusCode = (int)code;
            Headers = headers;
        }
        /// <summary>
        /// Api响应的状态码
        /// </summary>
        public virtual int StatusCode { get; }
        /// <summary>
        /// 响应头信息
        /// </summary>
        public virtual IDictionary<string, string> Headers { get;}
    }
}
