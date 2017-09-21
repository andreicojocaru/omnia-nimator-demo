using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;

namespace Nimator.Plugins.Couchbase.Settings
{
    class MemoryUsageCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public string PoolName { get; set; }

        public string BucketName { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public int AvailableMemoryThresholdPercentage { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseDataRetrieverSettings
            {
                Credentials = Credentials,
                ServerUrl = ServerUrl
            };

            return new MemoryUsageCheck(new CouchbaseDataRetriever(settings), AvailableMemoryThresholdPercentage, BucketName, PoolName);
        }
    }
}
