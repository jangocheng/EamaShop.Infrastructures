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
    /// <typeparam name="TResponse"></typeparam>
    public abstract class XmlApiParameter<TResponse> : FormatApiParameter<TResponse> where TResponse : ApiResponse
    {
        private const string XmlSupport = "text/plain";
       

        /// <inheritdoc />
        protected override bool CanSerialize(string content, string mediaType, Encoding encoding, HttpStatusCode code, IDictionary<string, string> headers)
        {
            return mediaType?.Equals(XmlSupport) == true;
        }
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
            // parse xml to json
            var document = new XmlDocument();
            document.LoadXml(content);

            content = JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented);
            var obj = Create(code, headers);
            return obj == null ? content.DeserializeJson<TResponse>() : content.DeserializeJson(obj);
        }
    }
}
