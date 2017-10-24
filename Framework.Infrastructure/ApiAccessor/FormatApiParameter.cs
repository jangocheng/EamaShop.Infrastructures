using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <inheritdoc />
    /// <summary>
    /// 表示可按指定格式进行转换的请求参数结果对象
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class FormatApiParameter<TResponse> : ApiParameter<TResponse> where TResponse : ApiResponse
    {
        /// <summary>
        /// 获取当前的类型对象是否可序列化
        /// </summary>
        /// <returns></returns>
        protected abstract bool CanSerialize(string content, string mediaType, Encoding encoding, HttpStatusCode code, IDictionary<string, string> headers);

        /// <inheritdoc />
        public override TResponse CreateResponse(string content, string contentType, HttpStatusCode code, IDictionary<string, string> headers)
        {
            Check.NotNull(contentType, nameof(contentType));
            var ps = contentType.Split(';');
            var mediaType = ps[0];
            var charset = "utf-8";
            if (ps.Length > 1)
            {
                var cs = ps[1];
                var ks = cs.Split('=');
                if (ks.Length > 1)
                {
                    charset = ks[1];
                }
            }
            var encoding = Encoding.GetEncoding(charset);
            var flag = CanSerialize(content, mediaType, encoding, code, headers);
            if (flag)
            {
                return Serialize(content, encoding, code, headers);
            }
            throw new NotSupportedException("不支持的响应类型");
        }
        /// <summary>
        /// 序列化为指定的数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        protected abstract TResponse Serialize(string content, Encoding encoding, HttpStatusCode code, IDictionary<string, string> headers);
    }
}
