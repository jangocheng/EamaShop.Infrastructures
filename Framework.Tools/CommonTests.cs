using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Framework.Infrastructure.Data;

namespace Framework.Tools
{
    public class CommonTests : BaseTests
    {
        [Fact]
        public void PageTest()
        {
            var factory = Service.GetRequiredService<IPageEnumerableFactory>();

            Assert.NotNull(factory);

            var enumerable = factory.CreatePageEnumerable(998, new[] { "", "", "" });

            Assert.NotNull(enumerable);

            Assert.Equal(enumerable.Count(), 3);

        }

        [Fact]
        public void SerializerTest()
        {
            var serializer = Get<ISerializer>();

            IEnumerable<IFoo> foo;

            var list = new List<IFoo>
            {
                new Foo1()
                {
                    Age = 1,
                    Display = "display name",
                    Name = "real name"
                },
                new Foo2
                {
                    Name = "real name",
                    Age = 2,
                    Higher = "hi hi hi "
                }
            };
            
            foo = list;

            var json = serializer.Serialize<IEnumerable<IFoo>>(foo);

            foo = serializer.Deserialize<IEnumerable<IFoo>>(json);

            Assert.NotNull(foo);

            Assert.NotNull(foo.FirstOrDefault()?.Name);
        }
        
        protected override void Configure(IServiceCollection service)
        {

        }
    }
}
