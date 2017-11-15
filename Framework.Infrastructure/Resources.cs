using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Framework.Infrastructure.Properties;
namespace Framework.Infrastructure
{
    /// <summary>
    /// 文案信息
    /// </summary>
    internal partial class Resources
    {
        public static string ApiClientResponseDescription(string clientName, HttpResponseMessage httpResponse)
        {
            
            return string.Format(Properties.Resources.Api_Response_Format,
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
