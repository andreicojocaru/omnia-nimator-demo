using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Models.Raw
{
    class RawBucketUsage
    {
        public RawBasicStats BasicStats { get; set; } 
    }

    class RawBasicStats
    {
        public int ItemCount { get; set; }
    }
}
