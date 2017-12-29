using System;
using System.Collections.Generic;
using System.Extensions;
using System.Text;
using Xunit;

namespace RedisLockTests
{
    public class InfrastructureTests
    {
        [Fact]
        public void ClrExtensions_Md5_Method_Test()
        {
            var source = "this is source need encryptor";

            var result = source.Md5();

        }
    }
}
