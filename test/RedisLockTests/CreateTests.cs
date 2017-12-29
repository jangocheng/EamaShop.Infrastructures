using EamaBuilder.Extensions.Lock.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace RedisLockTests
{
    public class CreateTests
    {
        [Fact]
        public void Redis_Lock_Test()
        {
            var services = new ServiceCollection().AddOptions().AddDistributedRedisLock(opt =>
            {
                opt.Configuration = "localhost:6379";
            }).BuildServiceProvider();

            var defaultLock = services.GetRequiredService<IDistributedLock>();

            for (int index = 0; index < 5; index++)
            {
                var flag = defaultLock.Enter(TimeSpan.FromSeconds(10));
                if (flag)
                {
                    Debug.Print("成功获取到分布式锁");
                }
                else
                {
                    Debug.Print("未能获取到分布式锁，该锁已经被占用");

                    Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(10)));

                    Assert.True(defaultLock.Enter(TimeSpan.FromSeconds(5)));
                    defaultLock.Exit();
                }
            }
        }
    }
}
