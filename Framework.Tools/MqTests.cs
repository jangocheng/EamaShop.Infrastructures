using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework.Infrastructure.MessageQueue;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Framework.Tools
{
    public class MqTests : BaseTests
    {
        [Fact]
        public void ProducerTest()
        {
            var producer = Get<IProducer<IFoo>>();

            Assert.NotNull(producer);

            async Task Send()
            {
                await producer.SendAsync(Create1());

                await producer.SendAsync(Create2());

                producer.Dispose();
            }

            Send().GetAwaiter().GetResult();
        }

        [Fact]
        public void ConsumerTest()
        {
            string name = null;

            var consumer = Get<IConsumer<IFoo>>();

            Assert.NotNull(consumer);

            void Consume(IFoo foo)
            {
                Assert.NotNull(foo);

                Debug.Print(foo.Name);

                name = foo.Name;
            }

            int i = 0;
            Task Checker()
            {
                return Task.Run(() =>
                {
                    while (true)
                    {
                        i++;
                        Thread.Sleep(1000);
                        if (!string.IsNullOrEmpty(name) || i == 10) break;
                    }
                });
            }

            consumer.Run(Consume);

            Task.WaitAll(Checker());

            Assert.NotNull(name);
        }

        protected override void Configure(IServiceCollection service)
        {
        }
    }
}
