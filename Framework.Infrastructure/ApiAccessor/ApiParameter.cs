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
    public abstract class ApiParameter<TRequest, TResult> where TResult : class
    {
        /// <summary>
        /// 获取请求的Api的Uri地址
        /// </summary>
        /// <returns></returns>
        public abstract Uri ApiUri { get; }
        /// <summary>
        /// 获取请求方式
        /// </summary>
        /// <returns></returns>
        public abstract HttpMethod Method { get; }
        /// <summary>
        /// 请求的参数上下文
        /// </summary>
        public abstract TRequest Parameters { get; }

        /// <summary>
        /// 从响应的字符串正文中创建与该请求相关联的响应实体
        /// </summary>
        /// <param name="content">正文内容</param>
        /// <param name="contentType">正文类型</param>
        /// <returns></returns>
        public abstract TResult CreateResult(string content, string contentType);
    }

}
