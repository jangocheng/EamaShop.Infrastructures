using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Framework.Tools
{
    public abstract class BaseTests
    {
        protected BaseTests()
        {
            var service = new ServiceCollection();
            service.AddOptions();
            service.AddLogging();
            Configure(service);
            service.AddInfrastructures();
            Service = service.BuildServiceProvider();
            Service.GetRequiredService<ILoggerFactory>().AddTraceSource("");
        }

        protected abstract void Configure(IServiceCollection service);
        public IServiceProvider Service { get; }

        protected T Get<T>()
        {
            return Service.GetRequiredService<T>();
        }

        protected IFoo Create1()
        {
            return new Foo1()
            {
                Age = 1,
                Display = "display name",
                Name = "this is nameer"
            };
        }

        protected IFoo Create2()
        {
            return new Foo2()
            {
                Name = "add",
                Age = 2,
                Higher = "hi hi hi "
            };
        }
    }
}
