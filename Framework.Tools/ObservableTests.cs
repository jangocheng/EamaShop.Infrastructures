using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Framework.Tools
{
    public class ObservableTests : BaseTests
    {
        [Fact]
        public void ObservableTest()
        {
            var observable = Service.GetRequiredService<IObservable<IFoo>>();
            observable.Subscribe(new Observer());
        }
        protected override void Configure(IServiceCollection service)
        {

        }
        private class Observer : IObserver<IFoo>
        {
            public void OnCompleted()
            {
                // log
            }

            public void OnError(Exception error)
            {
                // log
            }

            public void OnNext(IFoo value)
            {
                // do logic
            }
        }
    }
}
