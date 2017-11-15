using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Framework.Infrastructure.ApiAccessor;

namespace Framework.Tools
{
    public class BaiduApiParameter : ApiParameter<string, BaiduApiResponse>
    {
       


        public override Uri ApiUri => new Uri("https://www.baidu.com/index.html?id=2");
        public override HttpMethod Method => HttpMethod.Get;
        public override string Parameters => "";
        public override BaiduApiResponse CreateResult(string content, string contentType)
        {
            return new BaiduApiResponse();
        }
    }
}
