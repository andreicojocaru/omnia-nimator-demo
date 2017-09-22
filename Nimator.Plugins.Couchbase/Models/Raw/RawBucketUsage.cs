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
