using EamaShop.DataRespository.Internal;
using System;
using System.Threading;
using Xunit;

namespace EamaShop.DataRespository
{
    public class SearchEngineTests
    {
        [Fact]
        public void Test1()
        {
            var searchEngine = new InMemorySearchEngine();

            var page = searchEngine.SearchAsync("aaa", 1, 10).Result;

            var enumerator = page.GetEnumerator();

            var current = enumerator.Current;

            while (enumerator.MoveNext(cancellationToken: CancellationToken.None).Result)
            {
                current = enumerator.Current;
            }



        }
    }
}
