using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Framework.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <inheritdoc />
    /// <summary>
    ///  默认的Xml返回值的Api请求
    /// </summary>
    public abstract class XmlApiParameter<TRequest, TResult> : FormatApiParameter<TRequest, TResult> where TResult : class
    {
        private const string XmlSupport = "text/plain";


        /// <inheritdoc />
        protected override bool CanSerialize(string content, string mediaType, Encoding encoding)
        {
            return mediaType?.ToLower()?.Equals(XmlSupport) == true;
        }
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
            // parse xml to json
            var document = new XmlDocument();

            document.LoadXml(content);

            content = JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented);

            var obj = Create();

            return obj == null ? content.DeserializeJson<TResult>() : content.DeserializeJson(obj);
        }
    }
}
