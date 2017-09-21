using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;

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
            var settings = new CouchbaseDataRetrieverSettings
            {
                Credentials = Credentials,
                ServerUrl = ServerUrl
            };

            return new BucketAvailabilityCheck(new CouchbaseDataRetriever(settings), BucketName, PoolName);
        }
    }
}
