using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 文案信息
    /// </summary>
    internal class Resources
    {
        private const string ApiClientResponseDescriptionFormat =
            "客户端:{0}发送了请求,\r\n 请求的uri为:{1}; \r\n 请求的方式为:{2};\r\n 请求的参数正文为:{3} \r\n 响应的结果为:{4}; \r\n 状态码:{5};\r\n 正文类型为:{6}\r\n";
        public static string ApiClientResponseDescription(string clientName, HttpResponseMessage httpResponse)
        {
            return string.Format(ApiClientResponseDescriptionFormat,
                clientName,
                httpResponse.RequestMessage.RequestUri,
                httpResponse.RequestMessage.Method,
                httpResponse.RequestMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                httpResponse.StatusCode,
                httpResponse.Content.Headers.ContentType);
        }

        public static string ConfigureActionCannotBeNull(string configName)
        {
            return string.Format("配置项{0}不允许为空", configName);
        }
    }
}
