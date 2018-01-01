using EamaShop.Infrastructures.BLLModels;
using System;
using System.Collections.Generic;
using System.Extensions;
using System.Security.Claims;
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

            var identitys = new[]
            {
                new ClaimsIdentity(new []{
                    new Claim(ClaimTypes.Name,"1",ClaimValueTypes.String)
                },"jwt")
            };

            var fakerclaim = new ClaimsPrincipal(identitys);
            var name = fakerclaim.FindFirstValue<long>(ClaimTypes.Name);
        }
    }
}
