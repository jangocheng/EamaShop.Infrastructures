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
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class JsonApiParameter<TRequest, TResult> : FormatApiParameter<TRequest, TResult> where TResult : class
    {
        private const string JsonMediaType = "application/json";

        /// <summary>
        /// 创建一个 TResponse 的结果，如果返回值不为null，则填充该结果
        /// </summary>
        /// <returns></returns>
        protected virtual TResult Create()
        {
            return null;
        }
        /// <inheritdoc />
        protected override TResult Serialize(string content, Encoding encoding)
        {
            var obj = Create();

            if (obj == default(TResult))
            {
                return content.DeserializeJson<TResult>();
            }
            return content.DeserializeJson(obj);
        }
        /// <inheritdoc />
        protected override bool CanSerialize(string content, string mediaType, Encoding encoding)
        {
            Check.NotNull(mediaType, nameof(mediaType));

            return mediaType.ToLower().Equals(JsonMediaType);
        }
    }
}
