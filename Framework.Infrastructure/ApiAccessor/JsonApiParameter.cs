using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Framework.Infrastructure.Extensions;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <inheritdoc />
    /// <summary>
    /// 默认的Json返回值的Api请求
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class JsonApiParameter<TResponse> : FormatApiParameter<TResponse> where TResponse : ApiResponse
    {
        private const string JsonMediaType = "application/json";
       
        /// <summary>
        /// 创建一个 TResponse 的结果，如果返回值不为null，则填充该结果
        /// </summary>
        /// <returns></returns>
        protected virtual TResponse Create(HttpStatusCode code, IDictionary<string, string> headers)
        {
            return null;
        }
        /// <inheritdoc />
        protected override TResponse Serialize(string content, Encoding encoding, HttpStatusCode code, IDictionary<string, string> headers)
        {
            var obj = Create(code, headers);
            if (obj == null)
            {
                return content.DeserializeJson<TResponse>();
            }
            return content.DeserializeJson(obj);
        }
        /// <inheritdoc />
        protected override bool CanSerialize(string content, string mediaType, Encoding encoding, HttpStatusCode code, IDictionary<string, string> headers)
        {
            Check.NotNull(mediaType, nameof(mediaType));
            return mediaType.ToLower().Equals(JsonMediaType);
        }
    }
}
