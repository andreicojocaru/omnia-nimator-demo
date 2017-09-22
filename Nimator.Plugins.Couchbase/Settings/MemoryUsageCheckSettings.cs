using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Settings
{
    public class MemoryUsageCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public string PoolName { get; set; }

        public string BucketName { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public int AvailableMemoryThresholdPercentage { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseMemoryUsageSettings
            {
                ServerUrl = ServerUrl,
                Credentials = Credentials,
                AvailableMemoryThresholdPercentage = AvailableMemoryThresholdPercentage
            };

            if (!string.IsNullOrEmpty(PoolName))
            {
                settings.PoolName = PoolName;
            }

            if (!string.IsNullOrEmpty(BucketName))
            {
                settings.BucketName = BucketName;
            }

            return new MemoryUsageCheck(settings);
        }
    }
}
