using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Tools
{
    public class ZkConfigTests
    {
        [Fact]
        public void BuildTest()
        {

            var config = new ConfigurationBuilder().AddZookeeper("192.168.1.109:2181").Build();

            if (config.GetConnectionString("sss") == null)
            {
                var section = config.GetSection("ConnectionStrings");//.GetSection("Order");
                section.Value = "aaa";
            }
            config.GetSection("ss");
        }
    }
}
