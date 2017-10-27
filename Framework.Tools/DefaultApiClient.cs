using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.ApiAccessor;
using Microsoft.Extensions.Logging;

namespace Framework.Tools
{
    public class DefaultApiClient:ApiClient<DefaultConfiguration>
    {
        public DefaultApiClient( ILoggerFactory factory) : base(new DefaultConfiguration(), factory)
        {
        }
        

        protected override Task<HttpContent> CreateHttpContentAsync<TParameter, TResult>(ApiParameter<TParameter, TResult> parameter, HttpMethod method, Uri uri)
        {
            return Task.FromResult<HttpContent>(new StringContent(string.Empty));

        }
    }
}
