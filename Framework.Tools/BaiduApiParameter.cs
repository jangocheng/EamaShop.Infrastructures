using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Framework.Infrastructure.ApiAccessor;

namespace Framework.Tools
{
    public class BaiduApiParameter:ApiParameter<BaiduApiResponse>
    {
        public override Uri GetApiUri()
        {
            return new Uri("https://www.baidu.com");
        }

        public override HttpMethod GetMethod()
        {
            return HttpMethod.Get;
        }

        public override BaiduApiResponse CreateResponse(string content, string contentType, HttpStatusCode code, IDictionary<string, string> headers)
        {
            return new BaiduApiResponse(code, headers);
        }
    }
}
