using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Framework.Infrastructure.ApiAccessor;

namespace Framework.Tools
{
    public class BaiduApiResponse : ApiResponse
    {
        public BaiduApiResponse(HttpStatusCode code, IDictionary<string, string> headers) : base(code, headers)
        {
        }
    }
}
