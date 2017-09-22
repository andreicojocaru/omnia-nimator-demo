namespace Nimator.Plugins.Couchbase.Models.Settings
{
    public class CouchbaseMemoryUsageSettings : CouchbaseClusterSettings
    {
        public int AvailableMemoryThresholdPercentage { get; set; }
    }
}