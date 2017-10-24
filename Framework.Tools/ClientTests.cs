using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.ApiAccessor;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Framework.Tools
{
    public class ClientTests : BaseTests
    {
        [Fact]
        public void ApiClientTest()
        {

            var client = Get<IApiClient<DefaultConfiguration>>();

            async Task RunAsync()
            {
                var response = await client.SendAsync(new BaiduApiParameter());

                Assert.Equal(response.StatusCode, 200);
            }

            Task.WaitAll(RunAsync());
        }


        protected override void Configure(IServiceCollection service)
        {
            service.AddTransient<IApiClient<DefaultConfiguration>, DefaultApiClient>();
        }
    }
}
