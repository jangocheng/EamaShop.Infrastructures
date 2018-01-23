using EamaShop.Infrastructures.DataRespository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EamaShop.DataRespository.Internal
{
    [DebuggerDisplay("{Id} {Name}")]
    class DataItem : ISearchMetadata
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
