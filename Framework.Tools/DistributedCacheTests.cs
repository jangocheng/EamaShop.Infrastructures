using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;

namespace Framework.Tools
{
    public class DistributedCacheTests : BaseTests
    {
        [Fact]
        public void TTT_Test()
        {
            var value = new Foo1()
            {
                Age = 1,
                Display = "asdad",
                Name = "asdasdasdasd"
            };
            var redis = Service.GetRequiredService<IDistributedCache>();
            redis.SetAsync<IFoo>("key", value).GetAwaiter().GetResult();
            var result = redis.GetAsync<IFoo>("key").GetAwaiter().GetResult();
        }

        protected override void Configure(IServiceCollection service)
        {
            
            //service.AddDistributedRedisCache(opt =>
            //{
            //    opt.Configuration = "192.168.1.109:6379";
            //});

        }
    }
}
