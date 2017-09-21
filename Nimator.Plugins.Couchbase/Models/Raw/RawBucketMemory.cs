using System.Collections.Generic;

namespace Nimator.Plugins.Couchbase.Models.Raw
{
    class RawBucketMemory
    {
        public IList<RawNode> Nodes { get; set; }
    }

    class RawNode
    {
        public RawSystemStats SystemStats { get; set; }
    }

    class RawSystemStats
    {
        public long Mem_Free { get; set; }

        public long Mem_Total { get; set; }
    }
}
