using EamaShop.Infrastructures.DataRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.DataRespository.Internal
{
    class InMemorySearchEngine : SearchEngine<DataItem, string>
    {
        static InMemorySearchEngine()
        {
            var source = new DataItem[10000];
            for (int time = 0; time < 10000; time++)
            {
                source[time] = new DataItem()
                {
                    Id = time,
                    Name = time + "con"
                };
            }
            Source = source;
        }
        public static IEnumerable<DataItem> Source;
        protected override async Task<IEnumerable<DataItem>> SearchCoreAsync(string conditions, int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Source.Where(x => x.Id > conditions.Length).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArray();
        }

        protected override async Task<int> SearchTotal(string conditions, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Source.Count();
        }
    }
}
