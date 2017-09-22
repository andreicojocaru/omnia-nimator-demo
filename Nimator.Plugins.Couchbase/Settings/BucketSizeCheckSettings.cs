using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Settings
{
    public class BucketSizeCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public string PoolName { get; set; }

        public string BucketName { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public int MaxRecords { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseBucketSizeSettings
            {
                ServerUrl = ServerUrl,
                Credentials = Credentials,
                MaxRecords = MaxRecords
            };

            if (!string.IsNullOrEmpty(PoolName))
            {
                settings.PoolName = PoolName;
            }

            if (!string.IsNullOrEmpty(BucketName))
            {
                settings.BucketName = BucketName;
            }

            return new BucketSizeCheck(settings);
        }
    }
}
