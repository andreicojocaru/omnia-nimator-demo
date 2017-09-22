using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Settings
{
    public class BucketAvailabilityCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public string PoolName { get; set; }

        public string BucketName { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseClusterSettings
            {
                ServerUrl = ServerUrl,
                Credentials = Credentials
            };

            if (!string.IsNullOrEmpty(PoolName))
            {
                settings.PoolName = PoolName;
            }

            if (!string.IsNullOrEmpty(BucketName))
            {
                settings.BucketName = BucketName;
            }

            return new BucketAvailabilityCheck(settings);
        }
    }
}
